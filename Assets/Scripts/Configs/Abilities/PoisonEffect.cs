using System.Collections.Generic;
using Assets.Scripts.Core.SyncCodes;
using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Configs.Abilities
{
    [CreateAssetMenu(fileName = "PoisonEffect", menuName = "Configs/Ability/PoisonEffect")]
    public class PoisonEffect : AbilityEffectConfig
    {
        public IntAbilityParameter DamagePerTurn;

        public override bool Positive => false;


        protected override List<IAbilityParameter> GetParameters()
        {
            return new List<IAbilityParameter>()
            {
                DamagePerTurn,
                Duration
            };
        }

        public override void Register()
        {
        }

        public override ISyncScenarioItem Attach(CastContext castContext, int abilityLevel)
        {
            return null;
        }

        public override ISyncScenarioItem Apply(CastContext castContext, int abilityLevel)
        {
            return new ActionScenarioItem(() =>
                {
                    Debug.Log($"Applying poison to {castContext.Target}", castContext.Target);
                    DamageSystem.ApplyDamage(null, DamagePerTurn.GetValue(abilityLevel), castContext.Target);
                }
            ).PlayAndReturnSelf();
        }
    }
}