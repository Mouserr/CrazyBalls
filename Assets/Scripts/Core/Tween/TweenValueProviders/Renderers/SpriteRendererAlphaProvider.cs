using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;
using UnityEngine;
using Object = System.Object;

namespace Assets.Scripts.Core.Tween.TweenValueProviders.Renderers
{
    public class SpriteRendererAlphaProvider : IValueProvider<float>
    {
        #region Class fields
        private readonly SpriteRenderer renderer;
        #endregion

    
        #region Constructor
        public SpriteRendererAlphaProvider(Object obj)
        {
            renderer = obj as SpriteRenderer ?? ComponentHelper.GetComponent<SpriteRenderer>(obj);
        }
        #endregion


        #region Properties
        public float Value
        {
            get { return renderer.color.a; }
            set
            {
                Color color = renderer.color;
                renderer.color = ColorHelper.SetAlpha(value, color);
            }
        }
        #endregion
    }
}
