using System.Collections.Generic;
using Assets.Scripts.Core.Tween.TweenSimulators.SimulateFunctions;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;

namespace Assets.Scripts.Core.Tween.TweenSimulators
{
    public class FloatTweenSimulator : TweenSimulator<float>
    {
        #region Constructors
        public FloatTweenSimulator(object obj, float endValue, float duration, ISimulateFunction function, TweenType tweenType, TweenEndValueType tweenEndValueType)
            : this(GetProviders(obj, tweenType), endValue, duration, function, tweenEndValueType)
        {
        }

        public FloatTweenSimulator(IList<IValueProvider<float>> providers, float endValue, float duration, ISimulateFunction function, TweenEndValueType tweenEndValueType)
            : base(providers, endValue, duration, function, tweenEndValueType)
        {
        }
        #endregion

        #region Method overrides
        public override void Simulate(float time)
        {
            for (int i = 0; i < this.providers.Count; i++)
            {
                float currentTween = this.SimulateFunction.Invoke(time, 0.0f, this.shifts[i], this.duration);

                this.providers[i].Value += currentTween - previosValue[i];

                previosValue[i] = currentTween;
            }
        }

        public override void Init()
        {
			base.Init();

            switch (tweenEndValueType)
            {
                case TweenEndValueType.Shift:
                    for (int i = 0; i < providers.Count; i++)
                        shifts[i] = endValue;
                    break;
                case TweenEndValueType.On:
                    for (int i = 0; i < providers.Count; i++)
                        shifts[i] = (endValue - 1) * providers[i].Value;
                    break;
                default:
                    for (int i = 0; i < providers.Count; i++)
                        shifts[i] = endValue - providers[i].Value;
                    break;
            }

            for (int i = 0; i < this.previosValue.Length; i++)
                previosValue[i] = 0.0f;

        }

        #endregion
    }
}
