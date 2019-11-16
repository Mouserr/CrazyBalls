using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using Assets.Scripts.Core.Tween;
using Assets.Scripts.Core.Tween.TweenObjects;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyController : MonoBehaviour, IBall
    {
        private bool _active;
        private ISyncScenarioItem _collisionReaction;

        public float HPBarOffset = -0.35f;
        public int HealthPoints = 10;
        public event Action Death;
        public DamageEffect DamageEffect;

        public int Damage { get; }
        public Stat Health { get; set; }

        private void Awake()
        {
            Health = new Stat { MaxValue = HealthPoints, CurrentValue = HealthPoints };
            _active = true;
        }

        public void Die()
        {
            Death?.Invoke();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!_active)
            {
                return;
            }

            _collisionReaction?.Stop();
            _collisionReaction = new SyncScenario(new List<ISyncScenarioItem>
                {
                    new ActionScenarioItem(() =>
                    {
                        FMODUnity.RuntimeManager.PlayOneShot("event:/Damage");
                        if (DamageSystem.ApplyDamage(collision.transform.GetComponent<IBall>(), this))
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