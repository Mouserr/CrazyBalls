using UnityEngine;

namespace Assets.Scripts
{
    public static class SlowMotionController
    {
        private static float _prevDeltaTime;
        private static int _callsCount;
        public static bool SlowMoStarted { get; private set; }

        public static float CurrentScale
        {
            get { return SlowMoStarted ? 0.05f : 1; }
        }

        public static void Start()
        {
            _callsCount++;
            if (SlowMoStarted)
            {
                return;
            }

            SlowMoStarted = true;
            Time.timeScale = CurrentScale;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }

        public static void Stop()
        {
            _callsCount--;
            if (_callsCount > 0)
            {
                return;
            }

            Time.timeScale = CurrentScale;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            SlowMoStarted = false;
        }
    }
}