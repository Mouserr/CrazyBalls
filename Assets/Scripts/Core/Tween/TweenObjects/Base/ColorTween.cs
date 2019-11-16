using Assets.Scripts.Core.SyncCodes;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using Assets.Scripts.Core.Tween.TweenSimulators;
using Assets.Scripts.Core.Tween.TweenSimulators.SimulateFunctions;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenObjects.Base
{
    public class ColorTween : SimulateScenarioItem
    {
        #region Class fields
        private readonly Color endValue;
        private readonly TweenEndValueType endValueType;
        #endregion

        #region Constructor
        public ColorTween(object obj, Color endValue, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, 0, new EaseSimulateFunction(TweenPerformer.Ease[EaseType.EndValue]), endValueType, callback)
        {
        }
        public ColorTween(object obj, Color endValue, float duration, AnimationCurve curve, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, duration, new AnimationCurveSimulateFunction(curve), endValueType, callback)
        {        
        }

        public ColorTween(object obj, Color endValue, float duration, EaseFunction ease, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, duration, new EaseSimulateFunction(ease), endValueType, callback)
        {
        }

        public ColorTween(object obj, Color endValue, float duration, EaseType easeType, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, duration, new EaseSimulateFunction(TweenPerformer.Ease[easeType]), endValueType, callback)
        {
        }

        public ColorTween(object obj, Color endValue, float duration, ISimulateFunction function, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : base(new ColorTweenSimulator(obj, endValue, duration, function, TweenType.Color, endValueType), duration, null, callback)
        {
            this.endValue = endValue;
            this.endValueType = endValueType;
        }
        #endregion

        #region Properties
        public Color EndValue
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

