using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using Assets.Scripts.Core.Tween;
using Assets.Scripts.Core.Tween.TweenObjects;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class BattleScreen : AbstractScreen
    {
        [SerializeField]
        private Game _game;
        
        public override void Focus()
        {
            _game.StartGame();
            base.Focus();
        }

        public override ISyncScenarioItem GetShowTransition()
        {
            return new CompositeItem(base.GetShowTransition(),
                new MoveTween(_game, Vector3.zero, 0.3f, EaseType.Linear, TweenSpace.Local)
                );
        }

        protected override void PrepareToShow()
        {
            base.PrepareToShow();
            _game.transform.localPosition = new Vector3(6, 0 ,0);
        }

        public override ISyncScenarioItem GetHideTransition()
        {
            return new CompositeItem(base.GetHideTransition(),
                new MoveTween(_game, new Vector3(-6, 0 ,0), 0.3f, EaseType.Linear, TweenSpace.Local)
                );
        }

        public void Surrender()
        {
            _game.Clear();
            ScreensManager.Instance.OpenScreen(ScreenType.MainMenu);
        }
    }
}