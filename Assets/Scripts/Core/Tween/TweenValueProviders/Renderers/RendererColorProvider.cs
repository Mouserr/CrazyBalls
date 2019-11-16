using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;
using UnityEngine;
using Object = System.Object;

namespace Assets.Scripts.Core.Tween.TweenValueProviders.Renderers
{
    public class RendererColorProvider : IValueProvider<Color>
    {
        #region Class fields
        private readonly Material material;
        private readonly string fieldName; //_TintColor
        #endregion

        #region Constructor
        public RendererColorProvider(Object obj)
            : this(obj, "_Color")
        {
        }

        public RendererColorProvider(Object obj, string fieldName)
        {
            material = obj as Material ?? ComponentHelper.GetComponent<Renderer>(obj).material;
            this.fieldName = fieldName;
        }
        #endregion


        #region Properties
        public Color Value
        {
            get { return material.GetColor(fieldName); }
            set { material.SetColor(fieldName, value); }
        }
        #endregion
    }
}
