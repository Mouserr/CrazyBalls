using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenValueProviders
{
    public class TransformLocalRotationProvider : IValueProvider<Quaternion>
    {
        #region Class fields
        private readonly Transform component;
        #endregion

        #region Constructor
        public TransformLocalRotationProvider(System.Object obj)
        {
            component = ComponentHelper.GetComponent<Transform>(obj);
        }
        #endregion

        #region Properties
        public Quaternion Value
        {
            get { return null != component ? component.localRotation : Quaternion.Euler(Vector3.zero); }
            set
            {
                if (null != component)
                {
                    component.localRotation = value;
                }
            }
        }
        #endregion
    }
}
