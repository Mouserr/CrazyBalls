using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenValueProviders
{
    public class RectTransformSizeDeltaProvider : IValueProvider<Vector2>
    {
        #region Class fields
        private readonly RectTransform component;
        #endregion

        #region Constructor
        public RectTransformSizeDeltaProvider(System.Object obj)
        {
            component = ComponentHelper.GetComponent<RectTransform>(obj);
        }
        #endregion

        #region Properties
        public Vector2 Value
        {
            get { return component.sizeDelta; }
            set
            {
                if (null != component)
                {
                    component.sizeDelta = value;
                }
            }
        }
        #endregion
    }
}