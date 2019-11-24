using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Pools;
using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using Assets.Scripts.Core.Tween;
using Assets.Scripts.Core.Tween.TweenObjects;
using Assets.Scripts.UI;
using Assets.Scripts.Units;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class MapController : Singleton<MapController>
    {
        private readonly Dictionary<int, List<UnitController>> unitsByPlayer = new Dictionary<int, List<UnitController>>();
        private readonly List<ISyncScenarioItem> _allScenarios = new List<ISyncScenarioItem>();
        private int _movingUnitsCount;
        private int _firstPlayerPositionIndex;
        private int _secondPlayerPositionIndex;


        [SerializeField]
        private UnitController GokiController;

        [SerializeField]
        private UnitController MobController;

        [SerializeField]
        private UnitController BossController;

        [SerializeField]
        private GameObject _container;

        [SerializeField]
        private HealthBar _enemyHPPrefab;

        [SerializeField]
        private Transform[] _firstPlayerPositions;

        [SerializeField]
        private Transform[] _secondPlayerPositions;

        [SerializeField]
        private Transform _bossPosition;

        public GameObjectPool<HealthBar> HealthBarPool { get; private set; }
        public event Action<int> NoMoreUnitsAtMap;
        public event Action AllUnitsStopped;

        private void Awake()
        {
            HealthBarPool = new GameObjectPool<HealthBar>(_container, _enemyHPPrefab, 5);
            UnitsPool.Instance.Register(UnitType.Goki, GokiController, 3);
            UnitsPool.Instance.Register(UnitType.Mob, MobController, 6);
            UnitsPool.Instance.Register(UnitType.MobBoss, BossController, 1);
            Game.Instance.TurnPrepared += OnTurnPrepared;
        }

        public List<UnitController> GetUnits(int playerId)
        {
            if (unitsByPlayer.TryGetValue(playerId, out var units))
            {
                return units;
            }

            return null;
        }

        public void AddUnit(UnitController unit)
        {
            List<UnitController> playerUnits;
            if (!unitsByPlayer.TryGetValue(unit.PlayerId, out playerUnits))
            {
                playerUnits = new List<UnitController>();
                unitsByPlayer[unit.PlayerId] = playerUnits;
            }
            playerUnits.Add(unit);

            if (unit.PlayerId == 0)
            {
                unit.transform.position = _firstPlayerPositions[_firstPlayerPositionIndex++].position;
            }
            else
            {
                if (unit.Character.UnitType != UnitType.MobBoss)
                {
                    unit.transform.position = _secondPlayerPositions[_secondPlayerPositionIndex++].position;
                }
                else
                {
                    unit.transform.position = _bossPosition.position;
                }
            }
            unit.Init();
            unit.gameObject.SetActive(true);

           
            unit.Death += RemoveUnit;
            unit.MovementStateChanged += OnUnitMovementStateChanged;
        }

        public void RemoveUnit(UnitController unit)
        {
            List<UnitController> playerUnits;
            if (!unitsByPlayer.TryGetValue(unit.PlayerId, out playerUnits))
            {
                return;
            }

            playerUnits.Remove(unit);
            UnitsPool.Instance.ReleaseUnit(unit);
            unit.Death -= RemoveUnit;
            unit.MovementStateChanged -= OnUnitMovementStateChanged;
            if (playerUnits.Count == 0)
            {
                NoMoreUnitsAtMap?.Invoke(unit.PlayerId);
            }
        }

        public bool HasAvailableEnemy(UnitController askingUnit)
        {
            foreach (KeyValuePair<int, List<UnitController>> playerUnits in unitsByPlayer)
            {
                if (playerUnits.Key == askingUnit.PlayerId) continue;
                if (playerUnits.Value.Count > 0) return true;
            }

            return false;
        }

        public UnitController GetNearestEnemy(UnitController askingUnit)
        {
            float minSqrDistance = float.MaxValue;
            UnitController nearestUnit = null;
            var position = askingUnit.Position;
            foreach (KeyValuePair<int, List<UnitController>> playerUnits in unitsByPlayer)
            {
                if (playerUnits.Key == askingUnit.PlayerId) continue;
                foreach (UnitController unit in playerUnits.Value)
                {
                    float sqrDistance = (unit.Position - position).sqrMagnitude;
                    if (sqrDistance < minSqrDistance)
                    {
                        minSqrDistance = sqrDistance;
                        nearestUnit = unit;
                    }
                }
            }

            return nearestUnit;
        }

        public List<UnitController> GetEnemiesInArea(Vector2 position, float radius, int playerId)
        {
            float sqrRadius = radius * radius;
            List<UnitController> unitsInArea = new List<UnitController>();
            foreach (KeyValuePair<int, List<UnitController>> playerUnits in unitsByPlayer)
            {
                if (playerUnits.Key == playerId) continue;
                foreach (UnitController unit in playerUnits.Value)
                {
                    float sqrDistance = (unit.Position - position).sqrMagnitude;
                    if (sqrDistance < sqrRadius)
                    {
                        unitsInArea.Add(unit);
                    }
                }
            }

            return unitsInArea;
        }

        public void AddScenario(ISyncScenarioItem scenarioItem)
        {
            _allScenarios.Add(scenarioItem);
        }

        public void WaitForAllScenarios(Action callBack)
        {
            new SyncScenario(
                new SimpleConditionWaiter(() =>
                {
                    foreach (ISyncScenarioItem scenario in _allScenarios)
                    {
                        if (scenario == null || !scenario.IsComplete())
                        {
                            return false;
                        }
                    }

                    return _movingUnitsCount == 0;
                }),
                new ActionScenarioItem(callBack)).Play();
        }

        public void ResolveCollision(UnitController defender, Collision2D collision)
        {
            if (defender == Game.Instance.CurrentUnit || !defender.IsActive)
            {
                return;
            }

            var attacker = collision.transform.GetComponent<UnitController>();
            if (attacker == null || !attacker.IsActive)
            {
                return;
            }

            if (defender.PlayerId != attacker.PlayerId)
            {
                defender.CollisionReaction?.Stop();
                defender.CollisionReaction = new SyncScenario(new List<ISyncScenarioItem>
                    {
                        new ActionScenarioItem(() =>
                        {
                            FMODUnity.RuntimeManager.PlayOneShot("event:/Damage");
                            if (DamageSystem.ApplyPassiveDamage(attacker, defender))
                            {
                                defender.IsActive = false;
                            }
                        }),
                        new ActionScenarioItem(() =>
                        {
                            if (!defender.IsActive)
                            {
                                FMODUnity.RuntimeManager.PlayOneShot("event:/Death");
                                defender.Die();
                            }
                        }),
                        new ScaleTween(gameObject, Vector3.one, 1f, EaseType.Linear)
                            {TimeManager = GameSettings.AnimaitonTimeManager},

                    }, (scenario, force) => { transform.localScale = Vector3.one; })
                    {TimeManager = GameSettings.AnimaitonTimeManager};
                defender.CollisionReaction.Play();
            }
            else
            {
                defender.CastPassiveAbility(
                    new CastContext
                    {
                        Caster = defender,
                        Target = attacker
                    });
            }
        }

        public void Clear()
        {
            foreach (KeyValuePair<int, List<UnitController>> playerUnits in unitsByPlayer)
            {
                foreach (UnitController unit in playerUnits.Value)
                {
                    UnitsPool.Instance.ReleaseUnit(unit);
                }
                playerUnits.Value.Clear();
            }
            unitsByPlayer.Clear();
            _movingUnitsCount = 0;
            _firstPlayerPositionIndex = 0;
            _secondPlayerPositionIndex = 0;
        }

        private void OnTurnPrepared(UnitController unitController)
        {
            _allScenarios.Clear();
        }

        private void OnUnitMovementStateChanged(UnitController unitController, bool isMoving)
        {
            if (isMoving)
            {
                _movingUnitsCount++;
            }
            else
            {
                _movingUnitsCount--;
                if (_movingUnitsCount == 0)
                {
                    AllUnitsStopped?.Invoke();
                }
            }
        }
    }
}