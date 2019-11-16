using Assets.Scripts.Core.SyncCodes;
using Assets.Scripts.Core.Tween.TweenObjects.Base;
using Assets.Scripts.Core.Tween.TweenSimulators.SimulateFunctions;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenObjects
{
    public class MoveTween : Vector3Tween
    {
        #region Constructor
        public MoveTween(object obj, Vector3 endValue, TweenSpace space = TweenSpace.Global, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, 0, new EaseSimulateFunction(TweenPerformer.Ease[EaseType.EndValue]), space, endValueType, callback)
        {
        }

        public MoveTween(object obj, Vector3 endValue, float duration, AnimationCurve curve, TweenSpace space = TweenSpace.Global, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, duration, new AnimationCurveSimulateFunction(curve), space, endValueType, callback)
        {        
        }

        public MoveTween(object obj, Vector3 endValue, float duration, EaseFunction ease, TweenSpace space = TweenSpace.Global, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, duration, new EaseSimulateFunction(ease), space, endValueType, callback)
        {
        }

        public MoveTween(object obj, Vector3 endValue, float duration, EaseType easeType, TweenSpace space = TweenSpace.Global, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, duration, new EaseSimulateFunction(TweenPerformer.Ease[easeType]), space, endValueType, callback)
        {
        }

        public MoveTween(object obj, Vector3 endValue, float duration, ISimulateFunction function, TweenSpace space = TweenSpace.Global, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : base(obj, endValue, duration, function, TweenPerformer.GetShiftTypeBySpace(space), endValueType, callback)
        {
        }
        #endregion


        #region Static methods
        public static MoveTween Play(object obj, Vector3 endValue, TweenSpace space = TweenSpace.Global)
        {
            return (MoveTween)(new MoveTween(obj, endValue, space)).PlayAndReturnSelf();
        }

        public static MoveTween Play(object obj, Vector3 endValue, float duration, AnimationCurve curve, TweenSpace space = TweenSpace.Global, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
        {
            return Play(obj, endValue, duration, new AnimationCurveSimulateFunction(curve), space, endValueType, callback);
        }

        public static MoveTween Play(object obj, Vector3 endValue, float duration, EaseFunction ease, TweenSpace space = TweenSpace.Global, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
        {
            return Play(obj, endValue, duration, new EaseSimulateFunction(ease), space, endValueType, callback);
        }

        public static MoveTween Play(object obj, Vector3 endValue, float duration, EaseType easeType, TweenSpace space = TweenSpace.Global, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
        {
            return Play(obj, endValue, duration, new EaseSimulateFunction(TweenPerformer.Ease[easeType]), space, endValueType, callback);
        }

        public static MoveTween Play(object obj, Vector3 endValue, float duration, ISimulateFunction function, TweenSpace space = TweenSpace.Global, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
        {
            return (MoveTween)(new MoveTween(obj, endValue, duration, function, space, endValueType, callback)).PlayAndReturnSelf();
        }
        #endregion
    }
}
