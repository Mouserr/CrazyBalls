using Assets.Scripts.Core.SyncCodes;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using Assets.Scripts.Core.Tween.TweenSimulators;
using Assets.Scripts.Core.Tween.TweenSimulators.SimulateFunctions;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenObjects
{
    public class RotationTween : SimulateScenarioItem
    {
        #region Class fields
        private readonly Vector3 shiftValue;
        #endregion


        #region Constructor
        public RotationTween(object obj, Vector3 shiftValue, TweenSpace space = TweenSpace.Global, Callback callback = null)
			: this(obj, shiftValue, 0, new EaseSimulateFunction(TweenPerformer.Ease[EaseType.EndValue]), space, callback)
        {
        }

		public RotationTween(object obj, Vector3 shiftValue, float duration, AnimationCurve curve, TweenSpace space = TweenSpace.Global, Callback callback = null)
			: this(obj, shiftValue, duration, new AnimationCurveSimulateFunction(curve), space, callback)
        {
        }

		public RotationTween(object obj, Vector3 shiftValue, float duration, EaseFunction ease, TweenSpace space = TweenSpace.Global, Callback callback = null)
			: this(obj, shiftValue, duration, new EaseSimulateFunction(ease), space, callback)
        {
        }

		public RotationTween(object obj, Vector3 shiftValue, float duration, EaseType easeType, TweenSpace space = TweenSpace.Global, Callback callback = null)
			: this(obj, shiftValue, duration, new EaseSimulateFunction(TweenPerformer.Ease[easeType]), space, callback)
        {
        }

		public RotationTween(object obj, Vector3 shiftValue, float duration, ISimulateFunction function, TweenSpace tweenSpace = TweenSpace.Global, Callback callback = null)
			: base(new QuaternionTweenSimulator(obj, shiftValue, duration, function, tweenSpace == TweenSpace.Global ? TweenType.Rotation : TweenType.RotationLocal), duration, null, callback)
        {
            this.shiftValue = shiftValue;
        }
        #endregion


        #region Properties
        public Vector3 Value
        {
            get { return this.shiftValue; }
        }

        public TweenEndValueType EndValueType
        {
            get { return TweenEndValueType.Shift; }
        }
        #endregion


        #region Static methods
        public static RotationTween Play(object obj, Vector3 shiftValue, float duration, AnimationCurve curve, TweenSpace space = TweenSpace.Global, Callback callback = null)
        {
            return Play(obj, shiftValue, duration, new AnimationCurveSimulateFunction(curve), space, callback);
        }

        public static RotationTween Play(object obj, Vector3 shiftValue, float duration, EaseFunction ease, TweenSpace space = TweenSpace.Global, Callback callback = null)
        {
            return Play(obj, shiftValue, duration, new EaseSimulateFunction(ease), space, callback);
        }

        public static RotationTween Play(object obj, Vector3 shiftValue, float duration, EaseType easeType, TweenSpace space = TweenSpace.Global, Callback callback = null)
        {
            return Play(obj, shiftValue, duration, new EaseSimulateFunction(TweenPerformer.Ease[easeType]), space, callback);
        }

        public static RotationTween Play(object obj, Vector3 shiftValue, float duration, ISimulateFunction function, TweenSpace space = TweenSpace.Global, Callback callback = null)
        {
            return (RotationTween)(new RotationTween(obj, shiftValue, duration, function, space, callback)).PlayAndReturnSelf();
        }
        #endregion
    }
}