using System.Collections.Generic;
using Assets.Scripts.Core.Curves;
using Assets.Scripts.Core.Tween.TweenSimulators.SimulateFunctions;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenSimulators
{
    public class CurveTweenSimulator : TweenSimulator<Vector3>
    {
        private readonly AbstractCurve curve;

        #region Constructors
        public CurveTweenSimulator(AbstractCurve curve, object obj, float duration, ISimulateFunction function, TweenType tweenType)
            : this(curve, GetProviders(obj, tweenType), duration, function)
        {
        }
        public CurveTweenSimulator(AbstractCurve curve, IList<IValueProvider<Vector3>> providers, float duration, ISimulateFunction function)
            : base(providers, Vector3.zero, duration, function, TweenEndValueType.To)
        {
            this.curve = curve;
        }

        #endregion

        #region Method overrides
        public override void Simulate(float time)
        {
            for (int i = 0; i < this.providers.Count; i++)
            {
                this.providers[i].Value = curve.GetValue(this.SimulateFunction.Invoke(time, 0.0f, 1f, this.duration));
            }
        }

        public override void Init()
        {
			base.Init();
        }
        #endregion
    }
}
