using System.Collections.Generic;
using Assets.Scripts.Core;
using Assets.Scripts.Core.SyncCodes;
using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class ScreensManager : Singleton<ScreensManager>
    {
        private ISyncScenarioItem _currentTransition;
        private List<AbstractScreen> _openScreens = new List<AbstractScreen>();
        private Dictionary<ScreenType, AbstractScreen> _screensByType = new Dictionary<ScreenType, AbstractScreen>();

        [SerializeField]
        private List<AbstractScreen> _screens;

        public AbstractScreen TopScreen => _openScreens.Count > 0 ? _openScreens[_openScreens.Count - 1] : null;

        private void Awake()
        {
            foreach (var screen in _screens)
            {
                _screensByType[screen.ScreenType] = screen;
            }

            OpenScreen(ScreenType.MainMenu, 0);
        }

        public ISyncScenarioItem OpenScreen(ScreenType screenTypeType, float duration = 0.3f)
        {
            var screenToOpen = _screensByType[screenTypeType];
            _currentTransition?.Stop();

            var transitions = new List<ISyncScenarioItem>();
            if (!screenToOpen.IsPopup && TopScreen != null)
            {
                transitions.Add(TopScreen.GetHideTransition(duration));
            }
            
            transitions.Add(screenToOpen.GetShowTransition(duration));
            
            _currentTransition = new SyncScenario(
                new List<ISyncScenarioItem>()
                {
                    new CompositeItem(transitions)
                },
                (scenario, result) =>
                {
                    TopScreen?.UnFocus();
                    if (!screenToOpen.IsPopup && TopScreen != null)
                    {
                        _openScreens.Remove(TopScreen);
                    }
                    _openScreens.Add(screenToOpen);
                    screenToOpen.Focus();
                });
            
            return _currentTransition.PlayAndReturnSelf();
        }
    }
}