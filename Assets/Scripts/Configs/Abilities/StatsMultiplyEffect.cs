using System.Collections.Generic;
using Assets.Scripts.Core.SyncCodes;
using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Configs.Abilities
{
    [CreateAssetMenu(fileName = "StatsMultiplyEffect", menuName = "Configs/Ability/StatsMultiplyEffect")]
    public class StatsMultiplyEffect : AbilityEffectConfig
    {
        public CharacterStatType Stat;
        public bool IsPositive;
        public IntAbilityParameter Multiplicator;

        public override bool Positive => IsPositive;


        protected override List<IAbilityParameter> GetParameters()
        {
            return new List<IAbilityParameter>()
            {
                Multiplicator,
                Duration
            };
        }

        public override void Register()
        {
        }

        public override ISyncScenarioItem Attach(CastContext castContext, int abilityLevel)
        {
            return new ActionScenarioItem(() =>
                {
                    castContext.Target.Character.Stats[Stat].MultiplyBy(Multiplicator.GetValue(abilityLevel));
                }
            ).PlayRegisterAndReturnSelf();
        }

        public override ISyncScenarioItem Apply(CastContext castContext, int abilityLevel)
        {
            return new ActionScenarioItem(() =>
                {
                    castContext.Target.Character.Stats[Stat].MultiplyBy(Multiplicator.GetValue(abilityLevel));
                }
            ).PlayRegisterAndReturnSelf();
        }
    }
}