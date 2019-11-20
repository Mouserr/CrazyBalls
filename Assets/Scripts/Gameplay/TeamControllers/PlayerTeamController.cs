using UnityEngine;

namespace Assets.Scripts.TeamControllers
{
    // TODO: разделить на 2 класса для реализации сетевого режима
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

        public override void Clear()
        {
            DragController.Instance.Swipe -= OnSwipe;
            DragController.Instance.Deactivate();
        }

        private void OnSwipe(Vector2 direction, float speedCoef)
        {
            CurrentUnit.Move(direction, speedCoef);
            DragController.Instance.Swipe -= OnSwipe;
        }
    }
}