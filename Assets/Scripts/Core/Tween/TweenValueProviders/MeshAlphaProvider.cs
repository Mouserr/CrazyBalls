using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenValueProviders
{
    public class MeshAlphaProvider : IValueProvider<float>
    {
        #region Class fields
        private Mesh mesh;
        #endregion

        #region Constructor
        public MeshAlphaProvider(System.Object obj)
        {
            mesh = (Mesh) obj;
        }
        #endregion


        #region Properties
        public float Value {
            get
            {
                if (null != mesh)
                {
                    return mesh.colors.Length > 0 ? mesh.colors[0].a : 0;    
                }
                return 0;
            }
            set
            {
                if (null != mesh)
                {
                    Color[] colors = new Color[mesh.colors.Length];
                    for (int i = 0; i < mesh.colors.Length; i++)
                    {
                        colors[i] = ColorHelper.SetAlpha(value, mesh.colors[i]);
                    }
                    mesh.colors = colors;    
                }
            } 
        }
        #endregion

    
    }
}
