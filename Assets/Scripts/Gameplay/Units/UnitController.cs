﻿using System;
using System.Collections.Generic;
using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using Assets.Scripts.Core.Tween;
using Assets.Scripts.Core.Tween.TweenObjects;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts
{
    public class UnitController : MonoBehaviour
    {
        private Character _character;
        
        private Rigidbody2D _rigidbody;
        private bool _isMoving;
        private ProgressBar _healthBar;
        private List<UnitAura> _activeAuras = new List<UnitAura>();

        public float HPBarOffset = -0.35f;
        public event Action<UnitController> Death;
        public DamageEffect DamageEffect;

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

            Health.Changed += _healthBar.SetValue;
            Game.Instance.TurnStarted += OnTurnStarted;
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
            MapController.Instance.HealthBarPool.ReleaseObject(_healthBar);
            _character = null;
        }

        private void OnTurnStarted(UnitController currentUnit)
        {
            _character.OnTurnStart(new CastContext{ Target = this, Caster = this });
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