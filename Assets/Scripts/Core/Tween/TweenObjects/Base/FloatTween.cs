using Assets.Scripts.Core.SyncCodes;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using Assets.Scripts.Core.Tween.TweenSimulators;
using Assets.Scripts.Core.Tween.TweenSimulators.SimulateFunctions;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenObjects.Base
{
    public class FloatTween : SimulateScenarioItem
    {
        #region Class fields
        private readonly float endValue;
        private readonly TweenEndValueType endValueType;
        #endregion

        #region Constructor
        public FloatTween(object obj, float endValue, TweenType tweenType = TweenType.Undefined, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, 0, new EaseSimulateFunction(TweenPerformer.Ease[EaseType.EndValue]), tweenType, endValueType, callback)
        {
        }

        public FloatTween(object obj, float endValue, float duration, AnimationCurve curve, TweenType tweenType = TweenType.Undefined, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, duration, new AnimationCurveSimulateFunction(curve), tweenType, endValueType, callback)
        {
        }

        public FloatTween(object obj, float endValue, float duration, EaseFunction ease, TweenType tweenType = TweenType.Undefined, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, duration, new EaseSimulateFunction(ease), tweenType, endValueType, callback)
        {
        }

        public FloatTween(object obj, float endValue, float duration, EaseType easeType, TweenType tweenType = TweenType.Undefined, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, duration, new EaseSimulateFunction(TweenPerformer.Ease[easeType]), tweenType, endValueType, callback)
        {
        }

        public FloatTween(object obj, float endValue, float duration, ISimulateFunction function, TweenType tweenType = TweenType.Undefined, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : base(new FloatTweenSimulator(obj, endValue, duration, function, tweenType, endValueType), duration, null, callback)
        {
            this.endValue = endValue;
            this.endValueType = endValueType;
        }
        #endregion

        #region Properties
        public float EndValue
        {
            get { return this.endValue; }
        }

        public TweenEndValueType EndValueType
        {
            get { return this.endValueType; }
        }
        #endregion
    }
}
