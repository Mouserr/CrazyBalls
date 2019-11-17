using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class MainMenuScreen : AbstractScreen
    {
        public void StartBattle()
        {
            ScreensManager.Instance.OpenScreen(ScreenType.Battle);
        }

        public void OpenSettings()
        {
            Debug.LogError("Not implemented yet");
        }
    }
}