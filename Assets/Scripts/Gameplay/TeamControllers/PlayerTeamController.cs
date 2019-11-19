using UnityEngine;

namespace Assets.Scripts.TeamControllers
{
    public class PlayerTeamController : TeamController
    {
        public PlayerTeamController(int playerId) : base(playerId)
        {
        }

        public override void StartTurn(UnitController unit)
        {
            base.StartTurn(unit);
            DragController.Instance.Activate(unit.Position);
            DragController.Instance.Swipe += OnSwipe;
        }

        private void OnSwipe(Vector2 direction, float speedCoef)
        {
            CurrentUnit.Move(direction, speedCoef);
        }
    }
}