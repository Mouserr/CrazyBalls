using Assets.Scripts.Core.SyncCodes.SyncScenario;

namespace Assets.Scripts
{
    public class CharacterEffect
    {
        protected readonly ICharacter caster;
        protected ISyncScenarioItem castScenario;
        protected ISyncScenarioItem attachScenario;

        public int LeftTurns { get; set; }
        public int Level { get; private set; }
        public AbilityEffectConfig Config { get; private set; }

        public CharacterEffect(AbilityEffectConfig config, ICharacter caster, int level)
        {
            Config = config;
            this.caster = caster;
            Level = level;
            Config.Register();
        }

        public virtual ISyncScenarioItem Apply(CastContext castContext)
        {
            castScenario?.Stop();
            castScenario = Config.Apply(castContext, Level, caster);
            return castScenario;
        }
        
        public virtual ISyncScenarioItem Attach(CastContext castContext)
        {
            attachScenario?.Stop();
            attachScenario = Config.Attach(castContext, Level, caster);
            return attachScenario;
        }
    }
}