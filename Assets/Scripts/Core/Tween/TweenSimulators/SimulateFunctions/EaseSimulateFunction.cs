namespace Assets.Scripts.Core.Tween.TweenSimulators.SimulateFunctions
{
    public class EaseSimulateFunction : ISimulateFunction
    {
        private readonly EaseFunction function;

        public EaseSimulateFunction(EaseFunction function)
        {
            this.function = function;
        }
        public float Invoke(float time, float startValue, float endValue, float duration)
        {
            return duration == 0.0f ? endValue : (function(time / duration, 0, 1, 1) * (endValue - startValue) + startValue);
        }
    }
}