using System;
using System.Collections;
using Assets.Scripts.Core.Scenarios;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyController : MonoBehaviour, ICharacter
    {
        public float HPBarOffset = -0.35f;
        public int HealthPoints = 10;
        public event Action Death;

        public int Damage { get; }
        public Stat Health { get; set; }

        private void Awake()
        {
            Health = new Stat { MaxValue = HealthPoints, CurrentValue = HealthPoints };
        }

        public void Die()
        {
            Death?.Invoke();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            StartCoroutine(PerformHit(collision));
        }

        private IEnumerator PerformHit(Collision2D collision)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Damage");
            TimeController.StartSlowMo();
            yield return new WaitForSecondsRealtime(0.1f);
            TimeController.StopSlowMo();
            
            if (DamageSystem.ApplyDamage(collision.transform.GetComponent<ICharacter>(), this))
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Death");
                Game.Instance.Destroy(this);
            }
        }
    }
}