using System;
using System.Collections.Generic;
using Assets.Scripts.Core.SyncCodes;
using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using Assets.Scripts.Core.Tween;
using Assets.Scripts.Core.Tween.TweenObjects;
using Assets.Scripts.UI;
using DefaultNamespace;
using UnityEngine;

namespace Assets.Scripts
{
    public class UnitController : MonoBehaviour
    {
        private Character _character;
        
        private Rigidbody2D _rigidbody;
        private bool _isMoving;
        private HealthBar _healthBar;
        private List<UnitAura> _activeAuras = new List<UnitAura>();
        private ISyncScenarioItem _damageTakenItem;
        private int _prevHealthValue;
        
        public List<CharacterEffect> ActiveEffects { get; } = new List<CharacterEffect>();


        public float HPBarOffset = -0.35f;
        public event Action<UnitController> Death;
        public DamageEffect DamageEffect;
        public SpriteRenderer Sprite;
        public Transform Selection;

        public Character Character => _character;

        public CharacterStat Health => _character.Stats[CharacterStatType.Health];
        public CharacterStat Energy => _character.Stats[CharacterStatType.Energy];
        public CharacterStat PassiveDamage => _character.Stats[CharacterStatType.PassiveDamage];
        public CharacterStat MaxSpeed => _character.Stats[CharacterStatType.MaxSpeed];
        public Vector2 Position => transform.position;
        public int PlayerId { get; private set; }
        public bool IsActive { get; set; }
        public ISyncScenarioItem CollisionReaction { get; set; }

        public event Action<UnitController, bool> MovementStateChanged;

        public bool IsMoving
        {
            get { return _isMoving; }
            set
            {
                if (value == _isMoving)
                {
                    return;
                }

                _isMoving = value;
                if (_isMoving)
                {
                    _healthBar.gameObject.SetActive(false);
                }
                else
                {
                    UpdatePosition();
                    _healthBar.gameObject.SetActive(true);
                }
                MovementStateChanged?.Invoke(this, value);
            }
        }

        private void Awake()
        {
            _rigidbody = GetComponentInChildren<Rigidbody2D>();
        }

        public void SetCharacter(Character character, int playerId)
        {
            _character = _character ?? character;
            PlayerId = playerId;
            _prevHealthValue = _character.GetStat(CharacterStatType.Health);
            Sprite.sprite = _character.InGameSprite;
        }

        public void Init()
        {
            IsActive = true;
            _healthBar = MapController.Instance.HealthBarPool.GetObject();
            _healthBar.transform.SetParent(UIDragController.Instance.Canvas.transform);
            _healthBar.transform.localScale = Vector3.one;
            UpdatePosition();
            _healthBar.gameObject.SetActive(true);
            _healthBar.SetValue(1, true);
            _healthBar.BuffUpdated(ActiveEffects);

            Health.Changed += _healthBar.SetValue;
            Health.Changed += OnHealthChanged;
            Game.Instance.TurnStarted += OnTurnStarted;
        }

        public void OnHealthChanged(CharacterStat stat)
        {
            if (_prevHealthValue > stat.CurrentValue)
            {
                _damageTakenItem?.Stop();
                _damageTakenItem = DamageEffect.GetExplosionItem(0.3f).PlayRegisterAndReturnSelf();
            }
            
            _prevHealthValue = stat.CurrentValue;
        }

        public void Die()
        {
            Death?.Invoke(this);
        }

        public void Move(Vector2 direction, float speedCoef)
        {
            _rigidbody.velocity = direction * (MaxSpeed.CurrentValue * speedCoef);
            IsMoving = true;
        }

        public void AttachAura(UnitAura aura)
        {
            _activeAuras.Add(aura);
        }

        public void AddEffect(CharacterEffect effectConfig)
        {
            if (ActiveEffects.Find(x => x.Config.EffectType == effectConfig.Config.EffectType) == null)
            {
                ActiveEffects.Add(effectConfig);
                _healthBar.BuffUpdated(ActiveEffects);
            }
        }

        public ISyncScenarioItem CastAbility(CastContext castContext)
        {
            return Character.ActiveAbility.Apply(castContext);
        }

        public ISyncScenarioItem CastPassiveAbility(CastContext castContext)
        {
            return Character.PassiveAbility.Apply(castContext);
        }

        public void Clear()
        {
            Game.Instance.TurnStarted -= OnTurnStarted;
            Health.Changed -= _healthBar.SetValue;
            Health.Changed -= OnHealthChanged;;
            MapController.Instance.HealthBarPool.ReleaseObject(_healthBar);
            _character = null;
            ActiveEffects.Clear();
            foreach (var activeAura in _activeAuras)
            {
                Destroy(activeAura.gameObject);
            }
            _activeAuras.Clear();
        }

        private void RemoveEndedEffects()
        {
            for (int i = 0; i < ActiveEffects.Count;)
            {
                if (ActiveEffects[i].LeftTurns <= 0)
                {
                    ActiveEffects.RemoveAt(i);
                    continue;
                }

                i++;
            }
            _healthBar.BuffUpdated(ActiveEffects);
        }

        private void OnTurnStarted(UnitController currentUnit)
        {
            for (var i = 0; i < ActiveEffects.Count; i++)
            {
                var effect = ActiveEffects[i];
                effect.Apply(new CastContext {Target = this, Caster = this});
                if (_character == null)
                {
                    return;
                }
                effect.LeftTurns--;
            }

            RemoveEndedEffects();
            
            for (int i = 0; i < _activeAuras.Count;)
            {
                var activeAura = _activeAuras[i];
                if (activeAura.ShouldEnd(currentUnit))
                {
                    _activeAuras.RemoveAt(i);
                    Destroy(activeAura.gameObject);
                    continue;
                }

                i++;
            }
        }

        private void UpdatePosition()
        {
            _healthBar.transform.position = UIDragController.Instance.GetCanvasPosition(transform.position + new Vector3(0, HPBarOffset));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            MapController.Instance.ResolveCollision(this, collision);
        }
    }
}