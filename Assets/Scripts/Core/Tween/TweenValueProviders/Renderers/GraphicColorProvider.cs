using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

namespace Assets.Scripts.Core.Tween.TweenValueProviders.Renderers
{
    public class GraphicColorProvider : IValueProvider<Color>
    {
        #region Class fields
        private readonly MaskableGraphic graphics;
        #endregion

    
        #region Constructor
        public GraphicColorProvider(Object obj)
        {
            graphics = obj as MaskableGraphic ?? ComponentHelper.GetComponent<MaskableGraphic>(obj);
        }
        #endregion


        #region Properties
        public Color Value
        {
            get { return graphics.color; }
            set
            {
                graphics.color = value;
            }
        }
        #endregion
    }
}
