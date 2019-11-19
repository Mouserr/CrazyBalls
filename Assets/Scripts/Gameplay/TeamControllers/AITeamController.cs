namespace Assets.Scripts.TeamControllers
{
    public class AITeamController : TeamController
    {
        public AITeamController(int playerId) : base(playerId)
        {
        }

        public override void StartTurn(UnitController unit)
        {
            base.StartTurn(unit);

            CurrentUnit.CastAbility(new CastContext { CasterPoint = unit.Position });
        }
    }
}