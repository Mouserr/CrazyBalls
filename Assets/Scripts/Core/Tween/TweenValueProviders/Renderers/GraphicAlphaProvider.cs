using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

namespace Assets.Scripts.Core.Tween.TweenValueProviders.Renderers
{
    public class GraphicAlphaProvider : IValueProvider<float>
    {
        #region Class fields
        private readonly MaskableGraphic graphics;
        #endregion

    
        #region Constructor
        public GraphicAlphaProvider(Object obj)
        {
            graphics = obj as MaskableGraphic ?? ComponentHelper.GetComponent<MaskableGraphic>(obj);
        }
        #endregion


        #region Properties
        public float Value
        {
            get { return graphics.color.a; }
            set
            {
                Color color = graphics.color;
                graphics.color = ColorHelper.SetAlpha(value, color);
            }
        }
        #endregion
    }
}
