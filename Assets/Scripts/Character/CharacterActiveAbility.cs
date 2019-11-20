using Assets.Scripts.Configs;
using Assets.Scripts.Core.SyncCodes;
using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;

namespace Assets.Scripts
{
    public class CharacterActiveAbility : CharacterAbility
    {
        private readonly ActiveAbilityConfig activeAbilityConfig;
        private ISyncScenarioItem cooldownScenario;

        public CharacterActiveAbility(ActiveAbilityConfig config, ICharacter caster) : base(config, caster)
        {
            activeAbilityConfig = config;
        }

        public override ISyncScenarioItem Apply(CastContext castContext)
        {
            if (!CouldCast || !activeAbilityConfig.CouldApply(castContext, Level, caster))
            {
                return null;
            }
            var scenarioItem = base.Apply(castContext);
            CurrentCooldown = activeAbilityConfig.Cooldown.GetValue(Level);
            cooldownScenario = new IterateActionScenarioItem((leftTime) => { CurrentCooldown = leftTime; }, CurrentCooldown).PlayAndReturnSelf();
            return scenarioItem;
        }
    }
}