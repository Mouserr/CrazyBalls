using System.Collections.Generic;
using Assets.Scripts.Core.Tween.TweenSimulators.SimulateFunctions;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenSimulators
{
    public class ColorTweenSimulator : TweenSimulator<Color>
    {
        #region Constructors
        public ColorTweenSimulator(object obj, Color endValue, float duration, ISimulateFunction function, TweenType tweenType, TweenEndValueType tweenEndValueType)
            : this(GetProviders(obj, tweenType), endValue, duration, function, tweenEndValueType)
        {
        }

        public ColorTweenSimulator(IList<IValueProvider<Color>> providers, Color endValue, float duration, ISimulateFunction function, TweenEndValueType tweenEndValueType)
            : base(providers, endValue, duration, function, tweenEndValueType)
        {
        }
        #endregion

        #region Method overrides
        public override void Simulate(float time)
        {
            Color currentTween;
            for (int i = 0; i < this.providers.Count; i++)
            {
                //Mathf.Min(Mathf.Max(alpha, 0.0f), 1.0f) TODO! check is we need this hack.

                currentTween = new Color(
                    this.SimulateFunction.Invoke(time, 0.0f, this.shifts[i].r, this.duration),
                    this.SimulateFunction.Invoke(time, 0.0f, this.shifts[i].g, this.duration),
                    this.SimulateFunction.Invoke(time, 0.0f, this.shifts[i].b, this.duration),
                    this.SimulateFunction.Invoke(time, 0.0f, this.shifts[i].a, this.duration)
                    );

                this.providers[i].Value = new Color(
                    this.providers[i].Value.r - previosValue[i].r + currentTween.r,
                    this.providers[i].Value.g - previosValue[i].g + currentTween.g,
                    this.providers[i].Value.b - previosValue[i].b + currentTween.b,
                    this.providers[i].Value.a - previosValue[i].a + currentTween.a
                    );
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
                        shifts[i] = (endValue - Color.white) * providers[i].Value;
                    break;
                default:
                    for (int i = 0; i < providers.Count; i++)
                    {
                        shifts[i] = endValue - providers[i].Value;
                    }

                    break;
            }

            for (int i = 0; i < this.previosValue.Length; i++)
                previosValue[i] = Color.clear;
        }

        #endregion
    }
}
