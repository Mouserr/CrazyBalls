using System;
using System.Collections;
using Assets.Scripts.Core.Scenarios;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyController : MonoBehaviour
    {
        private Character _character;

        public float HPBarOffset = -0.35f;
        public int HealthPoints = 10;
        public event Action Death;

        public CharacterStat Energy => _character.Stats[CharacterStatType.Energy];
        public CharacterStat Damage => _character.Stats[CharacterStatType.PassiveDamage];
        public CharacterStat Health => _character.Stats[CharacterStatType.Health];
        public Character Character => _character;

        public void SetCharacter(Character character)
        {
            _character = _character??character;
        }

        private void Awake()
        {
         
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

            var attacker = collision.transform.GetComponent<AllyController>().Character;

            if (DamageSystem.ApplyDamage(attacker, _character))
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Death");
                Game.Instance.Destroy(this);
            }
        }
    }
}