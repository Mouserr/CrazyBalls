using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Tween.TweenSimulators.SimulateFunctions;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenSimulators
{
    public class QuaternionTweenSimulator : TweenSimulator<Quaternion>
    {
        private readonly Vector3[] floatShifts;
        private readonly Vector3[] floatPreviosValue;
        private readonly new Vector3 endValue; //TODO: Тут не было new. 

        #region Constructors
        public QuaternionTweenSimulator(object obj, Vector3 endValue, float duration, ISimulateFunction function, TweenType tweenType)
            : this(GetProviders(obj, tweenType), endValue, duration, function)
        {
        }

        public QuaternionTweenSimulator(IList<IValueProvider<Quaternion>> providers, Vector3 endValue, float duration, ISimulateFunction function)
            : base(providers, Quaternion.identity, duration, function, TweenEndValueType.Shift)
        {
            this.endValue = endValue;
            floatShifts = new Vector3[providers.Count];
            floatPreviosValue = new Vector3[providers.Count];
        }
        #endregion

        #region Method overrides
        public override void Simulate(float time)
        {
            for (int i = 0; i < providers.Count; i++)
            {
                Vector3 currentTween = new Vector3(SimulateFunction.Invoke(time, 0.0f, floatShifts[i].x, duration)
                    , SimulateFunction.Invoke(time, 0.0f, floatShifts[i].y, duration)
                    , SimulateFunction.Invoke(time, 0.0f, floatShifts[i].z, duration));

                Vector3 delta = currentTween - floatPreviosValue[i];
                providers[i].Value *= Quaternion.Euler(delta);
                floatPreviosValue[i] = currentTween;
            }
        }

        public override void Init()
        {
			base.Init();

            switch (tweenEndValueType)
            {
                case TweenEndValueType.Shift:
                    for (int i = 0; i < providers.Count; i++)
                        floatShifts[i] = endValue;
                    break;
                default:
                    Debug.LogErrorFormat("TweenEndValueType.{0} not suppored for Quaternions", tweenEndValueType);
                    break;
            }

            for (int i = 0; i < this.providers.Count; i++)
                floatPreviosValue[i] = Vector3.zero;
        }

        #endregion
    }
}