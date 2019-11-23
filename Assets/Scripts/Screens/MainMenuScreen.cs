using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class MainMenuScreen : AbstractScreen
    {
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