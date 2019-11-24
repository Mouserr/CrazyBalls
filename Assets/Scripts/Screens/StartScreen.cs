using FMODUnity;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class StartScreen : AbstractScreen
    {
        private void Start()
        {
            MusicController.Instance.ChangeTheme(false);
        }
        
        public void StartBattle()
        {
            ScreensManager.Instance.OpenScreen(ScreenType.MainMenu);
        }
    }
}