namespace Assets.Scripts.Core.Tween.TweenSimulators
{
    public interface ISimulateable
    {
        void Simulate(float time);
        void Init();
    }
}
