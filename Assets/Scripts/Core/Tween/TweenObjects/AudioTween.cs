using Assets.Scripts.Core.SyncCodes;
using Assets.Scripts.Core.Tween.TweenObjects.Base;
using Assets.Scripts.Core.Tween.TweenSimulators.SimulateFunctions;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenObjects
{
    public class AudioTween : FloatTween
    {
        #region Constructor
        public AudioTween(object obj, float endValue, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, 0, new EaseSimulateFunction(TweenPerformer.Ease[EaseType.EndValue]), endValueType, callback)
        {
        }

        public AudioTween(object obj, float endValue, float duration, AnimationCurve curve, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, duration, new AnimationCurveSimulateFunction(curve), endValueType, callback)
        {        
        }

        public AudioTween(object obj, float endValue, float duration, EaseFunction ease, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, duration, new EaseSimulateFunction(ease), endValueType, callback)
        {
        }

        public AudioTween(object obj, float endValue, float duration, EaseType easeType, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : this(obj, endValue, duration, new EaseSimulateFunction(TweenPerformer.Ease[easeType]), endValueType, callback)
        {
        }

        public AudioTween(object obj, float endValue, float duration, ISimulateFunction function, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
            : base(obj, endValue, duration, function, TweenType.Audio, endValueType,callback)
        {
        }
        #endregion


        #region Static methods
        public static AudioTween Play(object obj, float endValue, float duration, AnimationCurve curve, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
        {
            return Play(obj, endValue, duration, new AnimationCurveSimulateFunction(curve), endValueType, callback);
        }

        public static AudioTween Play(object obj, float endValue, float duration, EaseFunction ease, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
        {
            return Play(obj, endValue, duration, new EaseSimulateFunction(ease), endValueType, callback);
        }

        public static AudioTween Play(object obj, float endValue, float duration, EaseType easeType, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
        {
            return Play(obj, endValue, duration, new EaseSimulateFunction(TweenPerformer.Ease[easeType]), endValueType, callback);
        }

        public static AudioTween Play(object obj, float endValue, float duration, ISimulateFunction function, TweenEndValueType endValueType = TweenEndValueType.To, Callback callback = null)
        {
            return (AudioTween)(new AudioTween(obj, endValue, duration, function, endValueType, callback)).PlayAndReturnSelf();
        }
        #endregion

    }
}
