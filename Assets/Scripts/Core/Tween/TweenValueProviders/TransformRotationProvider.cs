using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenValueProviders
{
    public class TransformRotationProvider : IValueProvider<Quaternion> 
    {
        #region Class fields
        private readonly Transform component;
        #endregion

        #region Constructor
        public TransformRotationProvider(System.Object obj)
        {
            component = ComponentHelper.GetComponent<Transform>(obj);
        }
        #endregion

        #region Properties
        public Quaternion Value
        {
            get { return null != component? component.rotation : Quaternion.Euler(Vector3.zero); }
            set
            {
                if (null != component)
                {
                    component.rotation = value;    
                }
            }
        }
        #endregion
    }
}
