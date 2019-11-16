using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenValueProviders
{
    public class CameraRectPositionProvider : IValueProvider<Vector2>
    {
        #region Class fields
        private readonly Camera component;
        #endregion

        #region Constructor
        public CameraRectPositionProvider(System.Object obj)
        {
            component = ComponentHelper.GetComponent<Camera>(obj);
        }
        #endregion

        #region Properties
        public Vector2 Value
        {
            get { return null != component? component.rect.position : Vector2.zero; }
            set
            {
                if (null != component)
                {
                    component.rect = new Rect(
                        value.x,
                        value.y,
                        component.rect.width,
                        component.rect.height
                        );    
                }
            }
        }
        #endregion
    }
}
