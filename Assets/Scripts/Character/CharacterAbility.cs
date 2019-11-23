using Assets.Scripts.Configs;
using Assets.Scripts.Core.SyncCodes.SyncScenario;

namespace Assets.Scripts
{
    public class CharacterAbility
    {
        protected readonly ICharacter caster;
        protected ISyncScenarioItem castScenario;

        public float CurrentCooldown { get; protected set; }
        public int Level { get; private set; }
        public AbilityConfig Config { get; private set; }

        public bool CouldCast
        {
            get { return CurrentCooldown <= 0; }
        }

        public CharacterAbility(AbilityConfig config, ICharacter caster)
        {
            Config = config;
            this.caster = caster;
            Level = caster.Level;
            Config.Register();
        }

        public virtual ISyncScenarioItem Apply(CastContext castContext)
        {
            castScenario?.Stop();
            castScenario = Config.Apply(castContext, Level);
            return castScenario;
        }
    }
}
