using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Pools;
using Assets.Scripts.TeamControllers;
using Assets.Scripts.UI;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class Game : MonoBehaviour
    {
        private static Game _instance;
        private Player _player;
        private List<EnemyController> _enemies = new List<EnemyController>();
        private GameObjectPool<EnemyController> _enemiesPool;
        private GameObjectPool<ProgressBar> _enemiesHPPool;
        
        [SerializeField]
        private GameObject _container;
        [SerializeField]
        private EnemyController _enemyPrefab;
        [SerializeField]
        private ProgressBar _enemyHPPrefab;

        private Dictionary<int, TeamController> _controllers = new Dictionary<int, TeamController>();

        public static Game Instance
        {
            get { return _instance ?? (_instance = Object.FindObjectOfType<Game>()); }
        }

        private void Awake()
        {
            _enemiesPool = new GameObjectPool<EnemyController>(_container, _enemyPrefab, 5);
            _enemiesHPPool = new GameObjectPool<ProgressBar>(_container, _enemyHPPrefab, 5);
        }

        public void PrepareGame(TeamController firstController, TeamController secondController)
        {
            _controllers[firstController.PlayerId] = firstController;
            _controllers[secondController.PlayerId] = secondController;
        }

        public void SetupUpTeam(List<ICharacter> characters, int playerId)
        {

        }

        private void SpawnBall()
        {
            var gokiObj = GameObject.Find("Ball");

            var data = GameDictionary.AllyCharactersDictionary.FirstOrDefault();
            if (data == null)
            {
                throw new ArgumentNullException("data cannot be null");
            }
            var model = new AllyCharacter(data);
            gokiObj.GetComponent<UnitController>().SetCharacter(model);
        }

        private void SpawnEnemies()
        {
            for (int i = 0; i < 5; i++)
            {
                var enemy = _enemiesPool.GetObject();
                enemy.transform.SetParent(transform);
                enemy.transform.position = new Vector3(Random.Range(-2f,2f), Random.Range(-2f, 3f));

                var data = GameDictionary.EnemyCharactersDictionary.FirstOrDefault();
                if (data == null)
                {
                    throw new ArgumentNullException("data cannot be null");
                }
                var model = new EnemyCharacter(data);

                enemy.SetCharacter(model);
                enemy.gameObject.SetActive(true);
                _enemies.Add(enemy);
                var hpBar = _enemiesHPPool.GetObject();
                hpBar.transform.SetParent(DragController.Instance.Canvas.transform);
                hpBar.transform.localScale = Vector3.one;
                hpBar.transform.position = DragController.Instance.GetCanvasPosition(enemy.transform.position + new Vector3(0, enemy.HPBarOffset));
                hpBar.gameObject.SetActive(true);
                hpBar.SetValue(1, true);

                void OnHealthOnChanged(CharacterStat s) => hpBar.SetValue(s.Progress);

                enemy.Health.Changed += OnHealthOnChanged;

                void OnEnemyOnDeath()
                {
                    enemy.Health.Changed -= OnHealthOnChanged;
                    enemy.Death -= OnEnemyOnDeath;
                    _enemiesHPPool.ReleaseObject(hpBar);
                }

                enemy.Death += OnEnemyOnDeath;
            }
        }

        public void Destroy(UnitController unit)
        {
            unit.Die();
            _enemiesPool.ReleaseObject(unit);
        }

        public void Clear()
        {
            foreach (var enemy in _enemies)
            {
                Destroy(enemy);
            }
        }
    }
}
