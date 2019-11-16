using System;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenSimulators.SimulateFunctions
{
    public class AnimationCurveSimulateFunction : ISimulateFunction
    {
        private const float _TOLERANCE = 0.00001f;

        private readonly AnimationCurve curve;

        public AnimationCurveSimulateFunction(AnimationCurve curve)
        {
            this.curve = curve;

            if (Math.Abs(curve.Evaluate(0)) > _TOLERANCE)
                Debug.LogWarning("curve is not normalized : curve(0) != 0");
            if (Math.Abs(curve.Evaluate(1) - 1) > _TOLERANCE)
                Debug.LogWarning("curve is not normalized : curve(1) != 1");
        }
        public float Invoke(float time, float startValue, float endValue, float duration)
        {
            return duration == 0.0f ? endValue : curve.Evaluate(time/duration)*(endValue - startValue) + startValue;
        }
    }
}