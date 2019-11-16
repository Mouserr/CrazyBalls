using Assets.Scripts.Core.SyncCodes;
using Assets.Scripts.Core.Tween.TweenObjects.Base;
using Assets.Scripts.Core.Tween.TweenSimulators.SimulateFunctions;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenObjects
{
    public class AlphaTween : FloatTween
    {
        #region Constructor
        public AlphaTween(object obj, float endValue, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, 0, new EaseSimulateFunction(TweenPerformer.Ease[EaseType.EndValue]), endValueType, callback)
        {
        }

        public AlphaTween(object obj, float endValue, float duration, AnimationCurve curve, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, duration, new AnimationCurveSimulateFunction(curve), endValueType, callback)
        {        
        }

        public AlphaTween(object obj, float endValue, float duration, EaseFunction ease, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, duration, new EaseSimulateFunction(ease), endValueType, callback)
        {
        }

        public AlphaTween(object obj, float endValue, float duration, EaseType easeType, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, duration, new EaseSimulateFunction(TweenPerformer.Ease[easeType]), endValueType, callback)
        {
        }

        public AlphaTween(object obj, float endValue, float duration, ISimulateFunction function, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : base(obj, endValue, duration, function, TweenType.Alpha, endValueType, callback)
        {
        }
        #endregion

        #region Static methods
        public static AlphaTween Play(object obj, float endValue)
        {
            return (AlphaTween)(new AlphaTween(obj, endValue)).PlayAndReturnSelf();
        }

        public static AlphaTween Play(object obj, float endValue, float duration, AnimationCurve curve, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
        {
            return Play(obj, endValue, duration, new AnimationCurveSimulateFunction(curve), endValueType, callback);
        }

        public static AlphaTween Play(object obj, float endValue, float duration, EaseFunction ease, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
        {
            return Play(obj, endValue, duration, new EaseSimulateFunction(ease), endValueType, callback);
        }

        public static AlphaTween Play(object obj, float endValue, float duration, EaseType easeType, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
        {
            return Play(obj, endValue, duration, new EaseSimulateFunction(TweenPerformer.Ease[easeType]), endValueType, callback);
        }

        public static AlphaTween Play(object obj, float endValue, float duration, ISimulateFunction function, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
        {
            return (AlphaTween)(new AlphaTween(obj, endValue, duration, function, endValueType, callback)).PlayAndReturnSelf();
        }
        #endregion
    }
}
