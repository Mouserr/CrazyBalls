using Assets.Scripts.Core.SyncCodes.SyncScenario;

namespace Assets.Scripts
{
    public class CharacterEffect
    {
        protected ISyncScenarioItem castScenario;
        protected ISyncScenarioItem attachScenario;

        public int LeftTurns { get; set; }
        public int Level { get; private set; }
        public AbilityEffectConfig Config { get; private set; }

        public CharacterEffect(AbilityEffectConfig config, int level)
        {
            Config = config;
            Level = level;
            LeftTurns = Config.Duration.GetValue(level);
            Config.Register();
        }

        public virtual ISyncScenarioItem Apply(CastContext castContext)
        {
            castScenario?.Stop();
            castScenario = Config.Apply(castContext, Level);
            return castScenario;
        }
        
        public virtual ISyncScenarioItem Attach(CastContext castContext)
        {
            attachScenario?.Stop();
            attachScenario = Config.Attach(castContext, Level);
            return attachScenario;
        }
    }
}