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
            UIDragController.Instance.Activate(unit.Position);
            UIDragController.Instance.Swipe += OnSwipe;
        }

        public override void Clear()
        {
            UIDragController.Instance.Swipe -= OnSwipe;
            UIDragController.Instance.Deactivate();
        }

        private void OnSwipe(Vector2 direction, float speedCoef)
        {
            CurrentUnit.Move(direction, speedCoef);
            UIDragController.Instance.Swipe -= OnSwipe;
        }
    }
}