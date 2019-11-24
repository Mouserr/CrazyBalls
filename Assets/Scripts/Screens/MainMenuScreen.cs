using FMODUnity;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class MainMenuScreen : AbstractScreen
    {
        private void Start()
        {
            MusicController.Instance.ChangeTheme(false);
        }
        
        public void StartBattle()
        {
            ScreensManager.Instance.OpenScreen(ScreenType.StoryTell);
        }

        public void OpenSettings()
        {
            Debug.LogError("Not implemented yet");
        }
    }
}