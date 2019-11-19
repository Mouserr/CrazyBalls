using System;
using System.Collections.Generic;
using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using Assets.Scripts.Core.Tween;
using Assets.Scripts.Core.Tween.TweenObjects;
using UnityEngine;

namespace Assets.Scripts
{
    public class UnitController : MonoBehaviour
    {
        private Character _character;
        private bool _active;
        private ISyncScenarioItem _collisionReaction;
        private Rigidbody2D _rigidbody;

        public float HPBarOffset = -0.35f;
        public event Action Death;
        public DamageEffect DamageEffect;

        public Character Character => _character;

        public CharacterStat Health => _character.Stats[CharacterStatType.Health];
        public CharacterStat Energy => _character.Stats[CharacterStatType.Energy];
        public CharacterStat PassiveDamage => _character.Stats[CharacterStatType.PassiveDamage];
        public CharacterStat MaxSpeed => _character.Stats[CharacterStatType.MaxSpeed];
        public int PlayerId { get; private set; }
        public Vector2 Position => transform.localPosition;

        private void Awake()
        {
            _rigidbody = GetComponentInChildren<Rigidbody2D>();
        }

        public void SetCharacter(Character character, int playerId)
        {
            _character = _character ?? character;
            PlayerId = playerId;
            _active = true;
        }

        public void Die()
        {
            Death?.Invoke();
        }

        public void Move(Vector2 direction, float speedCoef)
        {
            _rigidbody.velocity = direction * (MaxSpeed.CurrentValue * speedCoef);
        }

        public void CastAbility(CastContext castContext)
        {
            Character.ActiveAbility.Apply(castContext);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var attacker = collision.transform.GetComponent<UnitController>().Character;

            _collisionReaction?.Stop();
            _collisionReaction = new SyncScenario(new List<ISyncScenarioItem>
                {
                    new ActionScenarioItem(() =>
                    {
                        FMODUnity.RuntimeManager.PlayOneShot("event:/Damage");
                        if (DamageSystem.ApplyDamage(attacker, _character))
                        {
                            _active = false;
                        }
                    }),
                    DamageEffect.GetExplosionItem(0.3f),
                    new ActionScenarioItem(() =>
                    {
                        if (!_active)
                        {
                            FMODUnity.RuntimeManager.PlayOneShot("event:/Death");
                            Game.Instance.Destroy(this);
                        }
                    }),
                    new ScaleTween(gameObject, Vector3.one, 1f, EaseType.Linear)
                        { TimeManager = GameSettings.AnimaitonTimeManager },

                }, (scenario, force) =>
                {
                    transform.localScale = Vector3.one;
                })
                { TimeManager = GameSettings.AnimaitonTimeManager };
            _collisionReaction.Play();
        }
    }
}