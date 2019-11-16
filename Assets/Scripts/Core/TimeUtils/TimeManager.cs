using UnityEngine;

namespace Assets.Scripts.Core.TimeUtils
{
    public class TimeManager : Singleton<TimeManager>, ITimeManager
    {
        public float GetDeltaTime()
        {
            return Time.deltaTime;
        }

        public float GetFixedDeltaTime()
        {
            return Time.fixedDeltaTime;
        }
    }
}