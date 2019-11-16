namespace Assets.Scripts.Core.Tween.TweenValueProviders.Base
{
    public interface IValueProvider<TValue>
    {
        TValue Value
        {
            get;
            set;
        }
    }
}
