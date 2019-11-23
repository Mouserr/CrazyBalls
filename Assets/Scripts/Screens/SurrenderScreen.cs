using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class SurrenderScreen : AbstractScreen
    {
        public void Ok()
        {
            ScreensManager.Instance.OpenScreen(ScreenType.StoryTell);
        }
    }
}