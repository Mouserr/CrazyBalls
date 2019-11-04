using System;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyController : MonoBehaviour, IBall
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
            if (DamageSystem.ApplyDamage(collision.transform.GetComponent<IBall>(), this))
            {
                Game.Instance.Destroy(this);
            }
        }
    }
}