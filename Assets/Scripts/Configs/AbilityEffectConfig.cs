using Assets.Scripts.Configs;
using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using Assets.Scripts.Models;

namespace Assets.Scripts
{
    public abstract class AbilityEffectConfig : AbilityConfig
    {
        public IntAbilityParameter Duration;
        
        public abstract bool Positive { get; }

        public abstract ISyncScenarioItem Attach(CastContext castContext, int abilityLevel, ICharacter targetToAttach);
    }
}