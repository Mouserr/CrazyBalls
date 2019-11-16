using Assets.Scripts.Core.Tween.TweenObjects.Base;
using Assets.Scripts.Core.Tween.TweenSimulators.SimulateFunctions;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenObjects
{
    public class ShortestRotationTween : Vector3Tween
    {
        #region Constructor
        public ShortestRotationTween(Transform obj, Vector3 endValue, float duration, EaseType function, TweenSpace space)
            : base(
                obj, getAngles(space == TweenSpace.Global? obj.eulerAngles : obj.localEulerAngles, endValue), duration, function,
                TweenPerformer.GetShortestRotationBySpace(space), TweenEndValueType.Shift, null)
        {
        }
		
		public ShortestRotationTween(Transform obj, Vector3 endValue, float duration, ISimulateFunction function, TweenSpace space)
            : base(
                obj, getAngles(space == TweenSpace.Global? obj.eulerAngles : obj.localEulerAngles, endValue), duration, function,
                TweenPerformer.GetShortestRotationBySpace(space), TweenEndValueType.Shift, null)
        {
        }

        public ShortestRotationTween(Rigidbody obj, Vector3 endValue, float duration, EaseType function)
            : base(
                obj, getAngles(obj.rotation.eulerAngles, endValue), duration, function,
                TweenPerformer.GetShortestRotationBySpace(TweenSpace.Global), TweenEndValueType.Shift, null)
        {
        }
        #endregion


        #region Auxiliary methods
        private static Vector3 getAngles(Vector3 from, Vector3 to)
        {
            return new Vector3(
                getAngle(from.x, to.x),
                getAngle(from.y, to.y),
                getAngle(from.z, to.z)
                );
        }

        private static float getAngle(float from, float to)
        {
            float delta = to - from;

            if (delta > 180)
                return delta - 360;

            if (delta < -180)
                return delta + 360;

            return delta;
        }
        #endregion
    }
}
