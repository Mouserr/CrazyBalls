using System.Collections.Generic;
using Assets.Scripts.Core.SyncCodes;
using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Configs.Abilities
{
    [CreateAssetMenu(fileName = "HealingTouch", menuName = "Configs/Ability/HealingTouch")]
    public class HealingTouch : AbilityConfig
    {
        public IntAbilityParameter HealAmount;
        
        public override void Register()
        {
        }

       
        public override ISyncScenarioItem Apply(CastContext castContext, int abilityLevel)
        {
            return new ActionScenarioItem(() =>
            {
                castContext.Target.Health.AddValue(HealAmount.GetValue(abilityLevel));
            }).PlayRegisterAndReturnSelf();
        }

        protected override List<IAbilityParameter> GetParameters()
        {
            return new List<IAbilityParameter>();
        }
    }
}