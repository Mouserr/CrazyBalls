using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;
using UnityEngine;
using Object = System.Object;

namespace Assets.Scripts.Core.Tween.TweenValueProviders
{
    public class LightIntensityProvider : IValueProvider<float>
    {
        #region Class fields
        private readonly Light light;
        #endregion

        #region Constructor
		public LightIntensityProvider(Object obj)
        {
			light = obj as Light ?? ComponentHelper.GetComponent<Light>(obj);
        }
        #endregion

        #region Properties
        public float Value
        {
			get { return light.intensity; }
			set { light.intensity = value; }
        }
        #endregion
    }
}
