using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenValueProviders
{
    public class CameraOrthoSizeProvider : IValueProvider<float>
    {
        #region Class fields
        private readonly Camera camera;
        #endregion

        #region Constructor
        public CameraOrthoSizeProvider(System.Object obj)
        {
            camera = ComponentHelper.GetComponent<Camera>(obj);
        }
        #endregion

        #region Properties
        public float Value
        {
            get { return camera != null && camera.orthographic ? camera.orthographicSize : 0; }
            set
            {
                if (camera != null && camera.orthographic)
                {
                    camera.orthographicSize = value;
                }
            }
        }
        #endregion
    }
}
