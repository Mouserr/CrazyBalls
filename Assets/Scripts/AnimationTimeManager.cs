using Assets.Scripts.Core.TimeUtils;
using UnityEngine;

namespace Assets.Scripts
{
    public class AnimationTimeManager : ITimeManager
    {
        public float GetDeltaTime()
        {
            return Time.deltaTime / SlowMotionController.CurrentScale;
        }

        public float GetFixedDeltaTime()
        {
            return Time.fixedDeltaTime / SlowMotionController.CurrentScale;
        }
    }
}