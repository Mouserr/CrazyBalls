using System.Collections.Generic;
using Assets.Scripts.Core.SyncCodes;
using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Configs.Abilities
{
    [CreateAssetMenu(fileName = "PoisonTouch", menuName = "Configs/Ability/PoisonTouch")]
    public class PoisonTouch : ActiveAbilityConfig
    {
        public UnitAura PoisonousAuraPrefab;
        public PoisonEffect EffectOnTouch;
        
        public override void Register()
        {
        }

        public override bool CouldApply(CastContext castContext, int abilityLevel)
        {
            return true;
        }

        public override ISyncScenarioItem Apply(CastContext castContext, int abilityLevel)
        {
            return new ActionScenarioItem(() =>
            {
                var unitAura = Instantiate(PoisonousAuraPrefab);
                unitAura.Attach(castContext.Caster, (holder, target) =>
                {
                    if (target.PlayerId != holder.PlayerId)
                    {
                        target.AddEffect(new CharacterEffect(EffectOnTouch, abilityLevel));
                    }
                });
                unitAura.SetSize(castContext.Caster.GetComponentInChildren<CircleCollider2D>().radius + 0.1f);
            }).PlayAndReturnSelf();

        }

        protected override List<IAbilityParameter> GetParameters()
        {
            return new List<IAbilityParameter>();
        }
    }
}