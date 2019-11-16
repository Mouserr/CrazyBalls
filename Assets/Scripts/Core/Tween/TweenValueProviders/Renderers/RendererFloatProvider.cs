using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;
using UnityEngine;
using Object = System.Object;

namespace Assets.Scripts.Core.Tween.TweenValueProviders.Renderers
{
    public class RendererFloatProvider : IValueProvider<float>
    {
        #region Class fields
        private readonly Material material;
        private readonly string fieldName; //_TintColor
        #endregion

        #region Constructor
        public RendererFloatProvider(Object obj, string feldName)
        {
            material = obj as Material ?? ComponentHelper.GetComponent<Renderer>(obj).material;
            fieldName = feldName;
        }
        #endregion

        #region Properties
        public virtual float Value
        {
            get { return material.GetFloat(fieldName); }
            set { material.SetFloat(fieldName, value); }
        }
        #endregion
    }
}
