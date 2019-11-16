using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenValueProviders.ShortestRotations
{
    public class TransformShortestRotationLocalProvider : IValueProvider<Vector3>
    {
        #region Class fields
        private readonly Transform component;
        #endregion

        #region Constructor
        public TransformShortestRotationLocalProvider(System.Object obj)
        {
            component = ComponentHelper.GetComponent<Transform>(obj);
        }
        #endregion

        #region Properties
        public Vector3 Value
        {
            get { return null != component ? component.localEulerAngles : Vector3.zero; }
            set
            {
                if (null != component)
                {
                    component.rotation = Quaternion.Euler(value);
                }
            }
        }
        #endregion
    }
}

