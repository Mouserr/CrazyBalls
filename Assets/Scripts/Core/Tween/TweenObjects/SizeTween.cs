using Assets.Scripts.Core.SyncCodes;
using Assets.Scripts.Core.Tween.TweenObjects.Base;
using Assets.Scripts.Core.Tween.TweenSimulators.SimulateFunctions;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenObjects
{
    public class SizeTween : Vector2Tween
    {
        #region Constructor
        public SizeTween(object obj, Vector2 endValue, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, 0, new EaseSimulateFunction(TweenPerformer.Ease[EaseType.EndValue]), endValueType, callback)
        {
        }

        public SizeTween(object obj, Vector2 endValue, float duration, AnimationCurve curve, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, duration, new AnimationCurveSimulateFunction(curve), endValueType, callback)
        {        
        }

        public SizeTween(object obj, Vector2 endValue, float duration, EaseFunction ease, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, duration, new EaseSimulateFunction(ease), endValueType, callback)
        {
        }

        public SizeTween(object obj, Vector2 endValue, float duration, EaseType easeType, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, duration, new EaseSimulateFunction(TweenPerformer.Ease[easeType]), endValueType, callback)
        {
        }

        public SizeTween(object obj, Vector2 endValue, float duration, ISimulateFunction function, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : base(obj, endValue, duration, function, TweenType.Size2D, endValueType, callback)
        {
        }
        #endregion

        #region Static methods
        public static SizeTween Play(object obj, Vector2 endValue, float duration, AnimationCurve curve, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
        {
            return Play(obj, endValue, duration, new AnimationCurveSimulateFunction(curve), endValueType, callback);
        }

        public static SizeTween Play(object obj, Vector2 endValue, float duration, EaseFunction ease, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
        {
            return Play(obj, endValue, duration, new EaseSimulateFunction(ease), endValueType, callback);
        }

        public static SizeTween Play(object obj, Vector2 endValue, float duration, EaseType easeType, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
        {
            return Play(obj, endValue, duration, new EaseSimulateFunction(TweenPerformer.Ease[easeType]), endValueType, callback);
        }

        public static SizeTween Play(object obj, Vector2 endValue, float duration, ISimulateFunction function, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
        {
            return (SizeTween)(new SizeTween(obj, endValue, duration, function, endValueType, callback)).PlayAndReturnSelf();
        }
        #endregion

    }
}
