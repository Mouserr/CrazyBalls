using System.Collections.Generic;
using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Configs.Abilities
{
    [CreateAssetMenu(fileName = "MeteorShowerConfig", menuName = "Configs/Ability/MeteorShower")]
    public class Explosion : ActiveAbilityConfig, ICastingAreaProvider
    {
        [SerializeField]
        private FloatAbilityParameter _radius;
        [SerializeField]
        private FloatAbilityParameter _damage;
        [SerializeField]
        private SpriteRenderer _explosionEffectPrefab;


        public float GetRadius(int abilityLevel)
        {
            return _radius.GetValue(abilityLevel);
        }

        public override void Register()
        {
        }

        public override bool CouldApply(CastContext castContext, int abilityLevel, ICharacter caster)
        {
            return true;
        }

        public override ISyncScenarioItem Apply(CastContext castContext, int abilityLevel, ICharacter caster)
        {
            var effect = PrefabHelper.Intantiate(_explosionEffectPrefab, Game.Instance.gameObject);
            effect.transform.position = castContext.CasterPoint;
            
            var targets = MapController.Instance.GetEnemiesInArea(castContext.TargetPoint,
                Radius.GetValue(abilityLevel), caster.PlayerId);

            for (int i = 0; i < castContext.Targets.Count; i++)
            {
                BattleManager.Instance.ApplyDamage(caster, 
                    Damage.GetValue(abilityLevel) / meteors.MeteorsPerSecondRange.Maximum, 
                    castContext.Targets[i]);
            }
          
            return null;
        }


        protected override List<IAbilityParameter> GetParameters()
        {
            return new List<IAbilityParameter>
            {
                Radius,
                Cooldown,
                Damage
            };
        }

    }
}