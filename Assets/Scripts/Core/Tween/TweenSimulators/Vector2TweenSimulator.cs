using System.Collections.Generic;
using Assets.Scripts.Core.Tween.TweenSimulators.SimulateFunctions;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenSimulators
{
    public class Vector2TweenSimulator : TweenSimulator<Vector2>
    {
        #region Constructors
        public Vector2TweenSimulator(object obj, Vector2 endValue, float duration, ISimulateFunction function, TweenType tweenType, TweenEndValueType tweenEndValueType)
            : this(GetProviders(obj, tweenType), endValue, duration, function, tweenEndValueType)
        {
        }

        public Vector2TweenSimulator(IList<IValueProvider<Vector2>> providers, Vector2 endValue, float duration, ISimulateFunction function, TweenEndValueType tweenEndValueType)
            : base(providers, endValue, duration, function, tweenEndValueType)
        {
        }
        #endregion

        #region Class fields
        private List<Vector2> values;
        #endregion

        #region Method overrides
        public override void Simulate(float time)
        {
            for (int i = 0; i < this.providers.Count; i++)
            {
                Vector2 currentTween = new Vector2(
                    SimulateFunction.Invoke(time, 0.0f, shifts[i].x, duration),
                    SimulateFunction.Invoke(time, 0.0f, shifts[i].y, duration)
                );
                values[i] = new Vector2(
                    values[i].x + (currentTween.x - previosValue[i].x),
                    values[i].y + (currentTween.y - previosValue[i].y)
                );
                providers[i].Value = values[i];
                
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
                        shifts[i] = Vector2.Scale(endValue - Vector2.one, providers[i].Value);
                    break;
                default:
                    for (int i = 0; i < providers.Count; i++)
                        shifts[i] = endValue - providers[i].Value;
                    break;
            }

            for (int i = 0; i < this.previosValue.Length; i++)
                previosValue[i] = Vector2.zero;


            values = new List<Vector2>();
            for (int i = 0; i < providers.Count; i++)
                values.Add(providers[i].Value);
        }
        #endregion
    }
}
