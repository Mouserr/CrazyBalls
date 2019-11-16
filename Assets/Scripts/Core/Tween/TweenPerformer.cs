using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.SyncCodes;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using Assets.Scripts.Core.Tween.TweenObjects;
using Assets.Scripts.Core.Tween.TweenObjects.Base;
using UnityEngine;

namespace Assets.Scripts.Core.Tween
{ 
    /* Типы изинга смотреть тут: http://easings.net/ru */
    public delegate float EaseFunction(float curTime, float startValue, float endValue, float duration);

    public sealed class TweenPerformer
    {
        #region Cass fields
        public static Dictionary<EaseType, EaseFunction> Ease;
        public static TweenPerformer instance;
        #endregion

        #region Constructors
        private TweenPerformer() { }

        static TweenPerformer()
        {
            Ease = new Dictionary<EaseType, EaseFunction>();

            Ease.Add(EaseType.Linear, Linear);
            Ease.Add(EaseType.QuadOut, QuadEaseOut);
            Ease.Add(EaseType.QuadIn, QuadEaseIn);
            Ease.Add(EaseType.QuadInOut, QuadEaseInOut);
            Ease.Add(EaseType.QuadOutIn, QuadEaseOutIn);
            Ease.Add(EaseType.ExpoOut, ExpoOut);
            Ease.Add(EaseType.ExpoIn, ExpoIn);
            Ease.Add(EaseType.ExpoInOut, ExpoInOut);
            Ease.Add(EaseType.ExpoOutIn, ExpoOutIn);
            Ease.Add(EaseType.CubicOut, CubicEaseOut);
            Ease.Add(EaseType.CubicIn, CubicEaseIn);
            Ease.Add(EaseType.CubicInOut, CubicEaseInOut);
            Ease.Add(EaseType.CubicOutIn, CubicEaseOutIn);
            Ease.Add(EaseType.QuartOut, QuartEaseOut);
            Ease.Add(EaseType.QuartIn, QuartEaseIn);
            Ease.Add(EaseType.QuartInOut, QuartEaseInOut);
            Ease.Add(EaseType.QuartOutIn, QuartEaseOutIn);
            Ease.Add(EaseType.QuintOut, QuintEaseOut);
            Ease.Add(EaseType.QuintIn, QuintEaseIn);
            Ease.Add(EaseType.QuintInOut, QuintEaseInOut);
            Ease.Add(EaseType.QuintOutIn, QuintEaseOutIn);
            Ease.Add(EaseType.CircOut, CircEaseOut);
            Ease.Add(EaseType.CircIn, CircEaseIn);
            Ease.Add(EaseType.CircInOut, CircEaseInOut);
            Ease.Add(EaseType.CircOutIn, CircEaseOutIn);
            Ease.Add(EaseType.SineOut, SineEaseOut);
            Ease.Add(EaseType.SineIn, SineEaseIn);
            Ease.Add(EaseType.SineInOut, SineEaseInOut);
            Ease.Add(EaseType.SineOutIn, SineEaseOutIn);
            Ease.Add(EaseType.ElasticOut, ElasticOut);
            Ease.Add(EaseType.ElasticIn, ElasticIn);
            Ease.Add(EaseType.ElasticInOut, ElasticInOut);
            Ease.Add(EaseType.ElasticOutIn, ElasticOutIn);
            Ease.Add(EaseType.BounceOut, BounceEaseOut);
            Ease.Add(EaseType.BounceIn, BounceEaseIn);
            Ease.Add(EaseType.BounceInOut, BounceEaseInOut);
            Ease.Add(EaseType.BounceOutIn, BounceEaseOutIn);
            Ease.Add(EaseType.BackOut, BackEaseOut);
            Ease.Add(EaseType.BackIn, BackEaseIn);
            Ease.Add(EaseType.BackInOut, BackEaseInOut);
            Ease.Add(EaseType.BackOutIn, BackEaseOutIn);
            Ease.Add(EaseType.EndValue, EndValue);
        }
        #endregion

        #region Properties
        public static TweenPerformer Instance
        {
            get { return instance ?? (instance = new TweenPerformer()); }
        }
        #endregion

        #region Ease functions
        #region Linear
        /// <summary>
        /// Easing equation function for a simple linear tweening, with no easing.
        /// </summary>
        public static float Linear(float curTime, float startValue, float endValue, float duration)
        {
            return endValue * curTime / duration + startValue;
        }
        #endregion

        #region Expo
        /// <summary>
        /// Easing equation function for an exponential (2^t) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        public static float ExpoOut(float curTime, float startValue, float endValue, float duration)
        {
            return (curTime == duration) ? startValue + endValue : endValue * (-Mathf.Pow(2, -10 * curTime / duration) + 1) + startValue;
        }

        /// <summary>
        /// Easing equation function for an exponential (2^t) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        public static float ExpoIn(float curTime, float startValue, float endValue, float duration)
        {
            return (curTime == 0) ? startValue : endValue * Mathf.Pow(2, 10 * (curTime / duration - 1)) + startValue;
        }

        /// <summary>
        /// Easing equation function for an exponential (2^t) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        public static float ExpoInOut(float curTime, float startValue, float endValue, float duration)
        {
            if (curTime == 0)
                return startValue;

            if (curTime == duration)
                return startValue + endValue;

            if ((curTime /= duration / 2) < 1)
                return endValue / 2 * Mathf.Pow(2, 10 * (curTime - 1)) + startValue;

            return endValue / 2 * (-Mathf.Pow(2, -10 * --curTime) + 2) + startValue;
        }

        /// <summary>
        /// Easing equation function for an exponential (2^t) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        public static float ExpoOutIn(float curTime, float startValue, float endValue, float duration)
        {
            if (curTime < duration / 2)
                return ExpoOut(curTime * 2, startValue, endValue / 2, duration);

            return ExpoIn((curTime * 2) - duration, startValue + endValue / 2, endValue / 2, duration);
        }
        #endregion

        #region Circular
        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        public static float CircEaseOut(float curTime, float startValue, float endValue, float duration)
        {
            return endValue * Mathf.Sqrt(1 - (curTime = curTime / duration - 1) * curTime) + startValue;
        }

        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        /// <returns>The correct value.</returns>
        public static float CircEaseIn(float curTime, float startValue, float endValue, float duration)
        {
            return -endValue * (Mathf.Sqrt(1 - (curTime /= duration) * curTime) - 1) + startValue;
        }

        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        public static float CircEaseInOut(float curTime, float startValue, float endValue, float duration)
        {
            if ((curTime /= duration / 2) < 1)
                return -endValue / 2 * (Mathf.Sqrt(1 - curTime * curTime) - 1) + endValue;

            return endValue / 2 * (Mathf.Sqrt(1 - (curTime -= 2) * curTime) + 1) + endValue;
        }

        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        public static float CircEaseOutIn(float curTime, float startValue, float endValue, float duration)
        {
            if (curTime < duration / 2)
                return CircEaseOut(curTime * 2, startValue, endValue / 2, duration);

            return CircEaseIn((curTime * 2) - duration, startValue + endValue / 2, endValue / 2, duration);
        }
        #endregion

        #region Quad
        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        public static float QuadEaseOut(float curTime, float startValue, float endValue, float duration)
        {
            return -endValue * (curTime /= duration) * (curTime - 2) + startValue;
        }

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        public static float QuadEaseIn(float curTime, float startValue, float endValue, float duration)
        {
            return endValue * (curTime /= duration) * curTime + startValue;
        }

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        public static float QuadEaseInOut(float curTime, float startValue, float endValue, float duration)
        {
            if ((curTime /= duration / 2) < 1)
                return endValue / 2 * curTime * curTime + startValue;

            return -endValue / 2 * ((--curTime) * (curTime - 2) - 1) + startValue;
        }

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        public static float QuadEaseOutIn(float curTime, float startValue, float endValue, float duration)
        {
            if (curTime < duration / 2)
                return QuadEaseOut(curTime * 2, startValue, endValue / 2, duration);

            return QuadEaseIn((curTime * 2) - duration, startValue + endValue / 2, endValue / 2, duration);
        }
        #endregion

        #region Sine
        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        public static float SineEaseOut(float curTime, float startValue, float endValue, float duration)
        {
            return endValue * Mathf.Sin(curTime / duration * (Mathf.PI / 2)) + startValue;
        }

        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        public static float SineEaseIn(float curTime, float startValue, float endValue, float duration)
        {
            return -endValue * Mathf.Cos(curTime / duration * (Mathf.PI / 2)) + endValue + startValue;
        }

        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        public static float SineEaseInOut(float curTime, float startValue, float endValue, float duration)
        {
            if ((curTime /= duration / 2) < 1)
                return endValue / 2 * (Mathf.Sin(Mathf.PI * curTime / 2)) + startValue;

            return -endValue / 2 * (Mathf.Cos(Mathf.PI * --curTime / 2) - 2) + startValue;
        }

        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing in/out: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        public static float SineEaseOutIn(float curTime, float startValue, float endValue, float duration)
        {
            if (curTime < duration / 2)
                return SineEaseOut(curTime * 2, startValue, endValue / 2, duration);

            return SineEaseIn((curTime * 2) - duration, startValue + endValue / 2, endValue / 2, duration);
        }
        #endregion

        #region Cubic
        /// <summary>
        /// Easing equation function for a cubic (t^3) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        public static float CubicEaseOut(float curTime, float startValue, float endValue, float duration)
        {
            return endValue * ((curTime = curTime / duration - 1) * curTime * curTime + 1) + startValue;
        }

        /// <summary>
        /// Easing equation function for a cubic (t^3) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        public static float CubicEaseIn(float curTime, float startValue, float endValue, float duration)
        {
            return endValue * (curTime /= duration) * curTime * curTime + startValue;
        }

        /// <summary>
        /// Easing equation function for a cubic (t^3) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        public static float CubicEaseInOut(float curTime, float startValue, float endValue, float duration)
        {
            if ((curTime /= duration / 2) < 1)
                return endValue / 2 * curTime * curTime * curTime + startValue;

            return endValue / 2 * ((curTime -= 2) * curTime * curTime + 2) + startValue;
        }

        /// <summary>
        /// Easing equation function for a cubic (t^3) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        public static float CubicEaseOutIn(float curTime, float startValue, float endValue, float duration)
        {
            if (curTime < duration / 2)
                return CubicEaseOut(curTime * 2, startValue, endValue / 2, duration);

            return CubicEaseIn((curTime * 2) - duration, startValue + endValue / 2, endValue / 2, duration);
        }
        #endregion

        #region Quartic
        /// <summary>
        /// Easing equation function for a quartic (t^4) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        public static float QuartEaseOut(float curTime, float startValue, float endValue, float duration)
        {
            return -endValue * ((curTime = curTime / duration - 1) * curTime * curTime * curTime - 1) + startValue;
        }

        /// <summary>
        /// Easing equation function for a quartic (t^4) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        public static float QuartEaseIn(float curTime, float startValue, float endValue, float duration)
        {
            return endValue * (curTime /= duration) * curTime * curTime * curTime + startValue;
        }

        /// <summary>
        /// Easing equation function for a quartic (t^4) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        public static float QuartEaseInOut(float curTime, float startValue, float endValue, float duration)
        {
            if ((curTime /= duration / 2) < 1)
                return endValue / 2 * curTime * curTime * curTime * curTime + startValue;

            return -endValue / 2 * ((curTime -= 2) * curTime * curTime * curTime - 2) + startValue;
        }

        /// <summary>
        /// Easing equation function for a quartic (t^4) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        public static float QuartEaseOutIn(float curTime, float startValue, float endValue, float duration)
        {
            if (curTime < duration / 2)
                return QuartEaseOut(curTime * 2, startValue, endValue / 2, duration);

            return QuartEaseIn((curTime * 2) - duration, startValue + endValue / 2, endValue / 2, duration);
        }
        #endregion

        #region Quintic
        /// <summary>
        /// Easing equation function for a quintic (t^5) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        public static float QuintEaseOut(float curTime, float startValue, float endValue, float duration)
        {
            return endValue * ((curTime = curTime / duration - 1) * curTime * curTime * curTime * curTime + 1) + startValue;
        }

        /// <summary>
        /// Easing equation function for a quintic (t^5) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        public static float QuintEaseIn(float curTime, float startValue, float endValue, float duration)
        {
            return endValue * (curTime /= duration) * curTime * curTime * curTime * curTime + startValue;
        }

        /// <summary>
        /// Easing equation function for a quintic (t^5) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        public static float QuintEaseInOut(float curTime, float startValue, float endValue, float duration)
        {
            if ((curTime /= duration / 2) < 1)
                return endValue / 2 * curTime * curTime * curTime * curTime * curTime + startValue;
            return endValue / 2 * ((curTime -= 2) * curTime * curTime * curTime * curTime + 2) + startValue;
        }

        /// <summary>
        /// Easing equation function for a quintic (t^5) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        public static float QuintEaseOutIn(float curTime, float startValue, float endValue, float duration)
        {
            if (curTime < duration / 2)
                return QuintEaseOut(curTime * 2, startValue, endValue / 2, duration);
            return QuintEaseIn((curTime * 2) - duration, startValue + endValue / 2, endValue / 2, duration);
        }
        #endregion

        #region Elastic
        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        public static float ElasticOut(float curTime, float startValue, float endValue, float duration)
        {
            if ((curTime /= duration) == 1)
                return startValue + endValue;

            float p = duration * 0.3f;
            float s = p / 4;

            return (endValue * Mathf.Pow(2, -10 * curTime) * Mathf.Sin((curTime * duration - s) * (2.0f * Mathf.PI) / p) + endValue + startValue);
        }

        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        public static float ElasticIn(float curTime, float startValue, float endValue, float duration)
        {
            if ((curTime /= duration) == 1)
                return startValue + endValue;

            float p = duration * 0.3f;
            float s = p / 4;

            return -(endValue * Mathf.Pow(2, 10 * (curTime -= 1)) * Mathf.Sin((curTime * duration - s) * (2 * Mathf.PI) / p)) + startValue;
        }

        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        public static float ElasticInOut(float curTime, float startValue, float endValue, float duration)
        {
            if ((curTime /= duration / 2) == 2)
                return startValue + endValue;

            float p = duration * (0.3f * 1.5f);
            float s = p / 4;

            if (curTime < 1)
                return -0.5f * (endValue * Mathf.Pow(2, 10 * (curTime -= 1)) * Mathf.Sin((curTime * duration - s) * (2 * Mathf.PI) / p)) + startValue;
            return endValue * Mathf.Pow(2, -10 * (curTime -= 1)) * Mathf.Sin((curTime * duration - s) * (2 * Mathf.PI) / p) * 0.5f + endValue + startValue;
        }

        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        public static float ElasticOutIn(float curTime, float startValue, float endValue, float duration)
        {
            if (curTime < duration / 2)
                return ElasticOut(curTime * 2, startValue, endValue / 2, duration);
            return ElasticIn((curTime * 2) - duration, startValue + endValue / 2, endValue / 2, duration);
        }
        #endregion

        #region Bounce
        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        public static float BounceEaseOut(float curTime, float startValue, float endValue, float duration)
        {
            if ((curTime /= duration) < (1.0f / 2.75f))
                return endValue * (7.5625f * curTime * curTime) + startValue;
            else if (curTime < (2.0f / 2.75f))
                return endValue * (7.5625f * (curTime -= (1.5f / 2.75f)) * curTime + 0.75f) + startValue;
            else if (curTime < (2.5f / 2.75f))
                return endValue * (7.5625f * (curTime -= (2.25f / 2.75f)) * curTime + 0.9375f) + startValue;
            else
                return endValue * (7.5625f * (curTime -= (2.625f / 2.75f)) * curTime + 0.984375f) + startValue;
        }

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        public static float BounceEaseIn(float curTime, float startValue, float endValue, float duration)
        {
            return endValue - BounceEaseOut(duration - curTime, 0.0f, endValue, duration) + startValue;
        }

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        public static float BounceEaseInOut(float curTime, float startValue, float endValue, float duration)
        {
            if (curTime < duration / 2.0f)
                return BounceEaseIn(curTime * 2.0f, 0.0f, endValue, duration) * 0.5f + startValue;
            else
                return BounceEaseOut(curTime * 2.0f - duration, 0.0f, endValue, duration) * 0.5f + endValue * 0.5f + startValue;
        }

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        public static float BounceEaseOutIn(float curTime, float startValue, float endValue, float duration)
        {
            if (curTime < duration / 2.0f)
                return BounceEaseOut(curTime * 2.0f, startValue, endValue / 2.0f, duration);
            return BounceEaseIn((curTime * 2.0f) - duration, startValue + endValue / 2.0f, endValue / 2.0f, duration);
        }
        #endregion

        #region Back
        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        public static float BackEaseOut(float curTime, float startValue, float endValue, float duration)
        {
            return endValue * ((curTime = curTime / duration - 1.0f) * curTime * ((1.70158f + 1.0f) * curTime + 1.70158f) + 1.0f) + startValue;
        }

        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        /// <param name="t">Current time in seconds.</param>
        /// <param name="b">Starting value.</param>
        /// <param name="c">Final value.</param>
        /// <param name="d">Duration of animation.</param>
        public static float BackEaseIn(float curTime, float startValue, float endValue, float duration)
        {
            return endValue * (curTime /= duration) * curTime * ((1.70158f + 1.0f) * curTime - 1.70158f) + startValue;
        }

        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        public static float BackEaseInOut(float curTime, float startValue, float endValue, float duration)
        {
            float s = 1.70158f;
            if ((curTime /= duration / 2.0f) < 1)
                return endValue / 2.0f * (curTime * curTime * (((s *= (1.525f)) + 1) * curTime - s)) + startValue;
            return endValue / 2.0f * ((curTime -= 2.0f) * curTime * (((s *= (1.525f)) + 1) * curTime + s) + 2) + startValue;
        }

        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        public static float BackEaseOutIn(float curTime, float startValue, float endValue, float duration)
        {
            if (curTime < duration / 2.0f)
                return BackEaseOut(curTime * 2.0f, startValue, endValue / 2.0f, duration);
            return BackEaseIn((curTime * 2.0f) - duration, startValue + endValue / 2.0f, endValue / 2.0f, duration);
        }


        #endregion

        #region End Value
        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        public static float EndValue(float curTime, float startValue, float endValue, float duration)
        {
            return endValue;
        }
        #endregion
        #endregion

        #region Tween functions
        #region Move
        [Obsolete("Create MoveTween by constructor")]
        public SimulateScenarioItem MoveTo(GameObject go, Vector3 destination, float duration, EaseType ease, TweenSpace space)
        {
            List<GameObject> goList = new List<GameObject>();
            goList.Add(go);
            return MoveTo(goList, destination, duration, ease, space, null);
        }

        [Obsolete("Create MoveTween by constructor")]
        public SimulateScenarioItem MoveTo(GameObject go, Vector3 destination, float duration, EaseType ease, TweenSpace space, Callback callback)
        {
            List<GameObject> goList = new List<GameObject>();
            goList.Add(go);
            return MoveTo(goList, destination, duration, ease, space, callback);
        }

        [Obsolete("Create MoveTween by constructor")]
        public SimulateScenarioItem MoveTo(List<GameObject> goList, Vector3 destination, float duration, EaseType ease, TweenSpace space, Callback callback = null)
        {
            SimulateScenarioItem tweenObj = null;

            if (goList != null && goList.Count > 0)
            {
                tweenObj = new MoveTween(convertToObjects(goList), destination, duration, Ease[ease], space, TweenEndValueType.To, callback);
                tweenObj.Play();
            }
            return tweenObj;
        }
        #endregion

        #region Shift
        [Obsolete("Create MoveTween by constructor")]
        public SimulateScenarioItem ShiftBy(GameObject go, Vector3 shift, float duration, EaseType ease, TweenSpace space)
        {
            List<GameObject> goList = new List<GameObject>();
            goList.Add(go);
            return ShiftBy(goList, shift, duration, ease, space, null);
        }

        [Obsolete("Create MoveTween by constructor")]
        public SimulateScenarioItem ShiftBy(GameObject go, Vector3 shift, float duration, EaseType ease, TweenSpace space, Callback callback)
        {
            List<GameObject> goList = new List<GameObject>();
            goList.Add(go);
            return ShiftBy(goList, shift, duration, ease, space, callback);
        }

        [Obsolete("Create MoveTween by constructor")]
        public SimulateScenarioItem ShiftBy(List<GameObject> goList, Vector3 shift, float duration, EaseType ease, TweenSpace space)
        {
            return ShiftBy(goList, shift, duration, ease, space, null);
        }

        [Obsolete("Create MoveTween by constructor")]
        public SimulateScenarioItem ShiftBy(List<GameObject> goList, Vector3 shift, float duration, EaseType ease, TweenSpace space, Callback callback)
        {
            SimulateScenarioItem tweenObj = null;

            if (goList != null && goList.Count > 0)
            {
                tweenObj = new MoveTween(convertToObjects(goList), shift, duration, Ease[ease], space, TweenEndValueType.Shift, callback);
                tweenObj.Play();
            }
            return tweenObj;
        }
        #endregion

        #region Fade
        [Obsolete("Create tween by constructor")]
        public SimulateScenarioItem Fade(GameObject go, float endOpaque, float duration, EaseType easeType)
        {
            return Fade(go, endOpaque, duration, easeType, null);
        }

        [Obsolete("Create tween by constructor")]
        public SimulateScenarioItem Fade(GameObject go, float endOpaque, float duration, EaseType easeType, Callback callback)
        {
            return Fade(new List<GameObject>{go}, endOpaque, duration, easeType, callback);
        }

        [Obsolete("Create tween by constructor")]
        public SimulateScenarioItem Fade(List<GameObject> goList, float endOpaque, float duration, EaseType easeType)
        {
            return Fade(goList, endOpaque, duration, easeType, null);
        }

        [Obsolete("Create AlphaTween by constructor")]
        public SimulateScenarioItem Fade(List<GameObject> goList, float endOpaque, float duration, EaseType easeType, Callback callback)
        {
            SimulateScenarioItem tweenObj = null;

            if (goList != null && goList.Count > 0)
            {
                tweenObj = new AlphaTween(convertToObjects(goList), endOpaque, duration, Ease[easeType], TweenEndValueType.To, callback);
                tweenObj.Play();
            }
            return tweenObj;
        }
        #endregion

        #region ScaleTo
        [Obsolete("Create ScaleTween by constructor")]
        public SimulateScenarioItem ScaleTo(GameObject go, Vector3 endScale, float duration, EaseType ease)
        {
            return ScaleTo(go, endScale, duration, ease, null);
        }

        [Obsolete("Create ScaleTween by constructor")]
        public SimulateScenarioItem ScaleTo(GameObject go, Vector3 endScale, float duration, EaseType ease, Callback callback)
        {
            return ScaleTo(new List<GameObject>{go}, endScale, duration, ease, callback);
        }

        [Obsolete("Create ScaleTween by constructor")]
        public SimulateScenarioItem ScaleTo(List<GameObject> goList, Vector3 endScale, float duration, EaseType ease)
        {
            return ScaleTo(goList, endScale, duration, ease, null);
        }

        [Obsolete("Create ScaleTween by constructor")]
        public SimulateScenarioItem ScaleTo(List<GameObject> goList, Vector3 endScale, float duration, EaseType ease, Callback callback)
        {
            SimulateScenarioItem tweenObj = null;

            if (goList != null && goList.Count > 0)
            {
                tweenObj = new ScaleTween(convertToObjects(goList), endScale, duration, Ease[ease], TweenEndValueType.To, callback);
                tweenObj.Play();
            }
            return tweenObj;
        }
        #endregion

        #region ScaleBy
        [Obsolete("Create ScaleTween by constructor")]
        public SimulateScenarioItem ScaleBy(GameObject go, Vector3 scaleShift, float duration, EaseType ease)
        {
            return ScaleBy(go, scaleShift, duration, ease, null);
        }

        [Obsolete("Create ScaleTween by constructor")]
        public SimulateScenarioItem ScaleBy(GameObject go, Vector3 scaleShift, float duration, EaseType ease, Callback callback)
        {
            List<GameObject> goList = new List<GameObject>();
            goList.Add(go);

            List<Vector3> shiftsList = new List<Vector3>();
            shiftsList.Add(scaleShift);

            return ScaleBy(goList, scaleShift, duration, ease, callback);
        }

        [Obsolete("Create ScaleTween by constructor")]
        public SimulateScenarioItem ScaleBy(List<GameObject> goList, Vector3 scaleShift, float duration, EaseType ease)
        {
            return ScaleBy(goList, scaleShift, duration, ease, null);
        }
    
        [Obsolete("Create ScaleTween by constructor")]
        public SimulateScenarioItem ScaleBy(List<GameObject> goList, Vector3 scaleShift, float duration, EaseType ease, Callback callback)
        {
            SimulateScenarioItem tweenObj = null;

            if (goList != null && goList.Count > 0)
            {
                tweenObj = new ScaleTween(convertToObjects(goList), scaleShift, duration, Ease[ease], TweenEndValueType.Shift, callback);
                tweenObj.Play();
            }
            return tweenObj;
        }

        #endregion

        #region ScaleOn
        [Obsolete("Create ScaleTween by constructor")]
        public SimulateScenarioItem ScaleOn(GameObject go, Vector3 scaleCoeff, float duration, EaseType ease)
        {
            return ScaleOn(go, scaleCoeff, duration, ease, null);
        }

        [Obsolete("Create ScaleTween by constructor")]
        public SimulateScenarioItem ScaleOn(GameObject go, Vector3 scaleCoeff, float duration, EaseType ease, Callback callback)
        {
            return ScaleOn(new List<GameObject>{go}, scaleCoeff, duration, ease, callback);
        }

        [Obsolete("Create ScaleTween by constructor")]
        public SimulateScenarioItem ScaleOn(List<GameObject> goList, Vector3 scaleCoeff, float duration, EaseType ease)
        {
            return ScaleOn(goList, scaleCoeff, duration, ease, null);
        }

        [Obsolete("Create ScaleTween by constructor")]
        public SimulateScenarioItem ScaleOn(List<GameObject> goList, Vector3 scaleCoeff, float duration, EaseType ease, Callback callback)
        {
            SimulateScenarioItem tweenObj = null;

            if (goList != null && goList.Count > 0)
            {
                tweenObj = new ScaleTween(convertToObjects(goList), scaleCoeff, duration, Ease[ease], TweenEndValueType.On, callback);
                tweenObj.Play();
            }
            return tweenObj;   
        }
        #endregion

        #region Color
        [Obsolete("Create ColorTween by constructor")]
        public SimulateScenarioItem ColorTo(GameObject go, Color endColor, float duration, EaseType ease)
        {
            return ColorTo(go, endColor, duration, ease, null);
        }

        [Obsolete("Create ColorTween by constructor")]
        public SimulateScenarioItem ColorTo(GameObject go, Color endColor, float duration, EaseType ease, Callback callback)
        {
            return ColorTo(new List<GameObject> { go }, endColor, duration, ease, callback);
        }

        [Obsolete("Create ColorTween by constructor")]
        public SimulateScenarioItem ColorTo(List<GameObject> goList, Color endColor, float duration, EaseType ease)
        {
            return ColorTo(goList, endColor, duration, ease, null);
        }

        [Obsolete("Create ColorTween by constructor")]
        public SimulateScenarioItem ColorTo(List<GameObject> goList, Color endColor, float duration, EaseType ease, Callback callback)
        {
            SimulateScenarioItem tweenObj = null;

            if (goList != null && goList.Count > 0)
            {
                tweenObj = new ColorTween(convertToObjects(goList), endColor, duration, Ease[ease], TweenEndValueType.To, callback);
                tweenObj.Play();
            }
            return tweenObj;
        }
        #endregion

        #region Size
        [Obsolete("Create SizeTween by constructor")]
        public SimulateScenarioItem SizeTo(GameObject go, Vector2 endSize, float duration, EaseType ease)
        {
            return SizeTo(go, endSize, duration, ease, null);
        }

        [Obsolete("Create SizeTween by constructor")]
        public SimulateScenarioItem SizeTo(GameObject go, Vector2 endSize, float duration, EaseType ease, Callback callback)
        {
            return SizeTo(new List<GameObject> { go }, endSize, duration, ease, callback);
        }

        [Obsolete("Create SizeTween by constructor")]
        public SimulateScenarioItem SizeTo(List<GameObject> goList, Vector2 endSize, float duration, EaseType ease)
        {
            return SizeTo(goList, endSize, duration, ease, null);
        }

        [Obsolete("Create SizeTween by constructor")]
        public SimulateScenarioItem SizeTo(List<GameObject> goList, Vector2 endSize, float duration, EaseType ease, Callback callback)
        {
            SimulateScenarioItem tweenObj = null;

            if (goList != null && goList.Count > 0)
            {
                tweenObj = new SizeTween(convertToObjects(goList), endSize, duration, Ease[ease], TweenEndValueType.To, callback);
                tweenObj.Play();
            }
            return tweenObj;
        }
        #endregion

        #endregion

        #region Utils
        public static TweenType GetShiftTypeBySpace(TweenSpace space)
        {
            switch (space)
            {
                case TweenSpace.Local:
                    return TweenType.MoveLocal;
                default:
                    return TweenType.Move;
            }
        }

        public static TweenType GetShortestRotationBySpace(TweenSpace space)
        {
            switch (space)
            {
                case TweenSpace.Local:
                    return TweenType.ShortestRotationLocal;
                default:
                    return TweenType.ShortestRotation;
            }
        }

        private List<Color> createColorList(Color color, int size)
        {
            List<Color> result = new List<Color>();
            for (int i = 0; i < size; i++)
            {
                result.Add(new Color(color.r, color.g, color.b, color.a));
            }
            return result;
        }

        public static List<Vector3> CreateVectorList(Vector3 vector, int size)
        {
            List<Vector3> result = new List<Vector3>();
            for (int i = 0; i < size; i++) {
                result.Add(new Vector3(vector.x, vector.y, vector.z));
            }
            return result;
        }

        public static List<Vector2> CreateVectorList(Vector2 vector, int size)
        {
            List<Vector2> result = new List<Vector2>();
            for (int i = 0; i < size; i++)
            {
                result.Add(new Vector3(vector.x, vector.y));
            }
            return result;
        }

        private List<System.Object> convertToObjects(List<GameObject> goList) // TODO: REMOVE THIS METHOD!!!
        {
            List<System.Object> objList = new List<System.Object>();
            for (int i = 0; i < goList.Count; i++)
            {
                if (goList[i] != null)
                    objList.Add((System.Object)goList[i]);
            }

            return objList;        
        }

        private List<Transform> convertToTransforms(List<GameObject> goList)
        {
            List<Transform> trList = new List<Transform>();
            for (int i = 0; i < goList.Count; i++)
            {
                if (goList[i] != null)
                    trList.Add(goList[i].transform);
            }

            return trList;
        }

        private Vector3 convertSizeToPoints(GameObject go, Vector3 origin, TweenSpace context)
        {
            return context == TweenSpace.Local ? go.GetSizeInGlobalPoints(origin) : origin;
        }

        private Vector3 convertPositionToPoints(GameObject go, Vector3 origin, TweenSpace context)
        {
            return context == TweenSpace.Local ? go.GetPositionInGlobalPoints(origin) : origin;
        }
        #endregion
    }
}