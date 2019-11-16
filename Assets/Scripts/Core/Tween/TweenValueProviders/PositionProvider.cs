using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenValueProviders
{
    public class PositionProvider : IValueProvider<Vector3> 
    {
        #region Class fields
        private readonly Transform component;
        #endregion
    
        #region Constructor
        public PositionProvider(System.Object obj)
        {
            component = ComponentHelper.GetComponent<Transform>(obj);
        }
        #endregion

        #region Properties
        public Vector3 Value
        {
            get { return null != component? component.position : Vector3.zero; }
            set {
                if (null != component)
                {
                    component.position = value;
                }
            }
        }
        #endregion
    }
}
