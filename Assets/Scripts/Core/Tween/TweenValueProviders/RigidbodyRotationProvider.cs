using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenValueProviders
{
    public class RigidbodyRotationProvider : IValueProvider<Quaternion> 
    {
        #region Class fields
        private readonly Rigidbody component;
        #endregion

        #region Constructor
        public RigidbodyRotationProvider(System.Object obj)
        {
            component = ComponentHelper.GetComponent<Rigidbody>(obj);
        }
        #endregion

        #region Properties
        public Quaternion Value
        {
            get { return null != component ? component.rotation : Quaternion.Euler(Vector3.zero); }
            set 
            {
                if (null != component)
                {
                    component.MoveRotation(value);
                } 
            }
        }
        #endregion
    }
}
