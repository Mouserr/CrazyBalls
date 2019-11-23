using Assets.Scripts.Configs;
using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using Assets.Scripts.Models;
using Configs;

namespace Assets.Scripts
{
    public abstract class AbilityEffectConfig : AbilityConfig
    {
        public IntAbilityParameter Duration;
        public EffectType EffectType;
        
        public abstract bool Positive { get; }

        public abstract ISyncScenarioItem Attach(CastContext castContext, int abilityLevel);

        public override ISyncScenarioItem Apply(CastContext castContext, int abilityLevel)
        {
            return null;
        }
    }
}