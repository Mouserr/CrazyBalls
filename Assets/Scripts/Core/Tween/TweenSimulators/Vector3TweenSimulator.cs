using System.Collections.Generic;
using Assets.Scripts.Core.Tween.TweenSimulators.SimulateFunctions;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenSimulators
{
    public class Vector3TweenSimulator : TweenSimulator<Vector3>
    {
        #region Constructors
        public Vector3TweenSimulator(object obj, Vector3 endValue, float duration, ISimulateFunction function, TweenType tweenType, TweenEndValueType tweenEndValueType)
            : this(GetProviders(obj, tweenType), endValue, duration, function, tweenEndValueType)
        {
        }
        public Vector3TweenSimulator(IList<IValueProvider<Vector3>> providers, Vector3 endValue, float duration, ISimulateFunction function, TweenEndValueType tweenEndValueType)
            : base(providers, endValue, duration, function, tweenEndValueType)
        {
        }
        #endregion

        #region Method overrides
        public override void Simulate(float time)
        {
            for (int i = 0; i < this.providers.Count; i++)
            {
                Vector3 currentTween = new Vector3(
                    this.SimulateFunction.Invoke(time, 0.0f, this.shifts[i].x, this.duration),
                    this.SimulateFunction.Invoke(time, 0.0f, this.shifts[i].y, this.duration),
                    this.SimulateFunction.Invoke(time, 0.0f, this.shifts[i].z, this.duration)
                    );

                Vector3 delta = currentTween - previosValue[i];

                this.providers[i].Value += delta;

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
                        shifts[i] = Vector3.Scale(endValue - Vector3.one, providers[i].Value);
                    break;
                default:
                    for (int i = 0; i < providers.Count; i++)
                        shifts[i] = endValue - providers[i].Value;
                    break;
            }

            for (int i = 0; i < previosValue.Length; i++)
                previosValue[i] = Vector3.zero;
        }

        #endregion
    }
}
