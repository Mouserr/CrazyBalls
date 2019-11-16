using Assets.Scripts.Core.SyncCodes;
using Assets.Scripts.Core.Tween.TweenObjects.Base;
using Assets.Scripts.Core.Tween.TweenSimulators.SimulateFunctions;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenObjects
{
    public class ScaleTween : Vector3Tween
    {
        #region Constructor
        public ScaleTween(object obj, Vector3 endValue, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, 0, new EaseSimulateFunction(TweenPerformer.Ease[EaseType.EndValue]), endValueType, callback)
        {
        }

        public ScaleTween(object obj, Vector3 endScale, float duration, AnimationCurve curve, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endScale, duration, new AnimationCurveSimulateFunction(curve), endValueType, callback)
        {        
        }

        public ScaleTween(object obj, Vector3 endScale, float duration, EaseFunction ease, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endScale, duration, new EaseSimulateFunction(ease), endValueType, callback)
        {
        }

        public ScaleTween(object obj, Vector3 endValue, float duration, EaseType easeType, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, duration, new EaseSimulateFunction(TweenPerformer.Ease[easeType]), endValueType, callback)
        {
        }

        public ScaleTween(object obj, Vector3 endScale, float duration, ISimulateFunction function, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : base(obj, endScale, duration, function, TweenType.Scale, endValueType, callback)
        {
        }
        #endregion


        #region Static methods
        public static ScaleTween Play(object obj, Vector3 endScale, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
        {
            return Play(obj, endScale, 0, new EaseSimulateFunction(TweenPerformer.Ease[EaseType.EndValue]), endValueType, callback);
        }

        public static ScaleTween Play(object obj, Vector3 endScale, float duration, AnimationCurve curve, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
        {
            return Play(obj, endScale, duration, new AnimationCurveSimulateFunction(curve), endValueType, callback);
        }

        public static ScaleTween Play(object obj, Vector3 endScale, float duration, EaseFunction ease, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
        {
            return Play(obj, endScale, duration, new EaseSimulateFunction(ease), endValueType, callback);
        }

        public static ScaleTween Play(object obj, Vector3 endValue, float duration, EaseType easeType, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
        {
            return Play(obj, endValue, duration, new EaseSimulateFunction(TweenPerformer.Ease[easeType]), endValueType, callback);
        }

        public static ScaleTween Play(object obj, Vector3 endScale, float duration, ISimulateFunction function, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
        {
            return (ScaleTween)(new ScaleTween(obj, endScale, duration, function, endValueType, callback)).PlayAndReturnSelf();
        }
        #endregion

    }
}
