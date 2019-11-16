using UnityEngine;

namespace Assets.Scripts
{
    public static class TimeController
    {
        private static float _prevDeltaTime;
        private static int _callsCount;
        public static bool SlowMoStarted { get; private set; }

        public static void StartSlowMo()
        {
            _callsCount++;
            if (SlowMoStarted)
            {
                return;
            }

            SlowMoStarted = true;
            Time.timeScale = 0.05f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }

        public static void StopSlowMo()
        {
            _callsCount--;
            if (_callsCount > 0)
            {
                return;
            }

            Time.timeScale = 1;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            SlowMoStarted = false;
        }
    }
}