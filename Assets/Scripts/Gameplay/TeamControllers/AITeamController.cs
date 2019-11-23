using Assets.Scripts.Core.SyncCodes;
using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;

namespace Assets.Scripts.TeamControllers
{
    public class AITeamController : TeamController
    {
        private ISyncScenarioItem _turnScenarioItem;

        public AITeamController(int playerId) : base(playerId)
        {
        }

        public override void StartTurn(UnitController unit)
        {
            base.StartTurn(unit);

            _turnScenarioItem = new SyncScenario(
                new CompleteScenarioItemConditionWaiter(
                    CurrentUnit.CastAbility(new CastContext {Caster = unit}),
                    true),
                new ActionScenarioItem(() => Game.Instance.NextTurn())
            ).PlayAndReturnSelf();
        }

        public override void Clear()
        {
            _turnScenarioItem?.Stop();
        }
    }
}