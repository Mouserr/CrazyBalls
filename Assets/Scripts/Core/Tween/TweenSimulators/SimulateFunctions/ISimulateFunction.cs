namespace Assets.Scripts.Core.Tween.TweenSimulators.SimulateFunctions
{
	public interface ISimulateFunction
    {
        float Invoke(float time, float startValue, float endValue, float duration);
    }
}