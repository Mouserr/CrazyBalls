using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenValueProviders
{
    public class RigidbodyPositionProvider : IValueProvider<Vector3> 
    {
        #region Class fields
        private readonly Rigidbody component;
        #endregion

        #region Constructor
        public RigidbodyPositionProvider(System.Object obj)
        {
            component = ComponentHelper.GetComponent<Rigidbody>(obj);
        }
        #endregion

        #region Properties
        public Vector3 Value
        {
            get { return null != component? component.position : Vector3.zero; }
            set { if (component != null) component.MovePosition(value); }
        }
        #endregion
    }
}
