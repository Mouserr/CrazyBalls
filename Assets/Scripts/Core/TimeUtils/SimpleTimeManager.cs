using UnityEngine;

namespace Assets.Scripts.Core.TimeUtils
{
    public class SimpleTimeManager : ITimeManager
    {
        public float TimeModifier = 1.0f;

        public float GetDeltaTime()
        {
            return Time.deltaTime * TimeModifier;
        }

        public float GetFixedDeltaTime()
        {
            return Time.fixedDeltaTime * TimeModifier;
        }
    }
}