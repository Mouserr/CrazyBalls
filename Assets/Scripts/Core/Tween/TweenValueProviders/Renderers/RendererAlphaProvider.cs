using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;
using UnityEngine;
using Object = System.Object;

namespace Assets.Scripts.Core.Tween.TweenValueProviders.Renderers
{
    public class RendererAlphaProvider : IValueProvider<float>
    {
        #region Class fields
        private readonly Material material;
        private readonly string fieldName; //_TintColor
        #endregion

    
        #region Constructor
        public RendererAlphaProvider(Object obj)
            : this(obj, "_Color")
        {
        }

        public RendererAlphaProvider(Object obj, string fieldName)
        {
            material = obj as Material ?? ComponentHelper.GetComponent<Renderer>(obj).material;
            this.fieldName = fieldName;
        }
        #endregion


        #region Properties
        public float Value
        {
            get { return material.GetColor(fieldName).a; }
            set
            {
                Color color = material.GetColor(fieldName);
                material.SetColor(fieldName, ColorHelper.SetAlpha(value, color));
            }
        }
        #endregion
    }
}
