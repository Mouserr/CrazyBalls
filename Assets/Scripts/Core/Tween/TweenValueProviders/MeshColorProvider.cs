using System;
using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;
using UnityEngine;

namespace Assets.Scripts.Core.Tween.TweenValueProviders
{
    public class MeshColorProvider : IValueProvider<Color>
    {
        #region Class fields
        private readonly MeshFilter component;
        private Color defaultColor;
        #endregion

    
        #region Constructor
        public MeshColorProvider(System.Object obj)
            : this(obj, Color.clear)
        {
            component = ComponentHelper.GetComponent<MeshFilter>(obj);
        }

        public MeshColorProvider(System.Object obj, Color defaultColor)
        {
            component = ComponentHelper.GetComponent<MeshFilter>(obj);
            this.defaultColor = defaultColor;
        }
        #endregion


        #region Properties
        public Color Value
        {
            get
            {
                Color[] colors = component.mesh.colors;
                return colors.Length > 0 ? colors[0] : defaultColor;
            }
            set
            {
                Color[] colors = new Color[component.mesh.vertexCount];
                Array.Copy(component.mesh.colors, colors, colors.Length);
                for (int i = 0; i < colors.Length; i++)
                    colors[i] = value;

                component.mesh.colors = colors;
            }
        }
        #endregion    
    }
}