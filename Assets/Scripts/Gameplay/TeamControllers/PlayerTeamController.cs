using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Core.SyncCodes;
using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using Assets.Scripts.Core.Tween;
using Assets.Scripts.Core.Tween.TweenObjects;
using UnityEngine;

namespace Assets.Scripts.TeamControllers
{
    // TODO: разделить на 2 класса для реализации сетевого режима
    public class PlayerTeamController : TeamController
    {
        private ISyncScenarioItem _selectionAnimation;
        private bool _showSelection;

        public PlayerTeamController(int playerId) : base(playerId)
        {
        }

        public override void StartTurn(UnitController unit)
        {
            base.StartTurn(unit);
            _selectionAnimation?.Stop();
            _selectionAnimation = GetSelectionAnimation().PlayAndReturnSelf();

            UIDragController.Instance.Activate(unit.Position);
            UIDragController.Instance.Swipe += OnSwipe;
        }

        private SyncScenario GetSelectionAnimation()
        {

            return new SyncScenario(
                new List<ISyncScenarioItem> { 
                    new ScaleTween(CurrentUnit.Selection, Vector3.one),
                    new ScaleTween(CurrentUnit.Selection, Vector3.one * 1.3f, 0.3f, EaseType.BounceInOut),
                    new ScaleTween(CurrentUnit.Selection, Vector3.one, 0.2f, EaseType.QuadIn),
                    new TimeWaiterScenarioItem(0.4f)
                },
                (s, interrupted) =>
                {
                    new ScaleTween(CurrentUnit.Selection, Vector3.one * 0.5f).Play();
                    if (!interrupted)
                    {
                        _selectionAnimation = GetSelectionAnimation().PlayAndReturnSelf();
                    }
                });
        }

        private void StopSelection()
        {
            if (_selectionAnimation != null)
            {
                _selectionAnimation.Stop();
            }
        }

        public override void Clear()
        {
           StopSelection();
            UIDragController.Instance.Swipe -= OnSwipe;
            UIDragController.Instance.Deactivate();
        }

        private void OnSwipe(Vector2 direction, float speedCoef)
        {
            StopSelection();
            CurrentUnit.Move(direction, speedCoef);
            UIDragController.Instance.Swipe -= OnSwipe;
        }
    }
}