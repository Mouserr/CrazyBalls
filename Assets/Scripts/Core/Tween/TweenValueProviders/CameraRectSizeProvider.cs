using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenValueProviders
{
    public class CameraRectSizeProvider : IValueProvider<Vector2> 
    {
        #region Class fields
        private readonly Camera component;
        #endregion

        #region Constructor
        public CameraRectSizeProvider(System.Object obj)
        {
            component = ComponentHelper.GetComponent<Camera>(obj);
        }
        #endregion

        #region Properties
        public Vector2 Value
        {
            get { return component != null ? component.rect.size : Vector2.zero; }
            set
            {
                if (null != component)
                {
                    Rect rect = component.rect;
                    rect.size = value;

                    component.rect = rect;
                }
            }
        }
        #endregion
    }
}
