using Assets.Scripts.Core.TimeUtils;

namespace Assets.Scripts
{
    public class GameSettings
    {
        public static ITimeManager AnimaitonTimeManager { get; } = new AnimationTimeManager();
    }
}