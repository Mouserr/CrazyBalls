using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Core.Tween.TweenValueProviders.Renderers;
using Assets.Scripts.Core.Tween.TweenValueProviders.ShortestRotations;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Core.Tween.TweenValueProviders.Base
{
    public class ValueProviderBuilder
    {
        #region Class fields
        private readonly Dictionary<TweenType, List<KeyValuePair<Type, Type>>> providerOptions = new Dictionary<TweenType, List<KeyValuePair<Type, Type>>>();
        private static ValueProviderBuilder instance;
        #endregion

        #region Constructor
        private ValueProviderBuilder()
        {
            this.init();
        }
        #endregion

        #region Properties
        public static ValueProviderBuilder Instance
        {
            get { return instance ?? (instance = new ValueProviderBuilder()); }
        }
        #endregion

        #region Methods
        private void init()
        {
            AddProvider(TweenType.Alpha, typeof(Mesh), typeof(MeshAlphaProvider));
            AddProvider(TweenType.Alpha, typeof(SpriteRenderer), typeof(SpriteRendererAlphaProvider));
            //AddProvider(TweenType.Alpha, typeof(Renderer), typeof(RendererAlphaProvider));
            AddProvider(TweenType.Alpha, typeof(MaskableGraphic), typeof(GraphicAlphaProvider));


            AddProvider(TweenType.MoveRectPosition, typeof(Camera), typeof(CameraRectPositionProvider));
            AddProvider(TweenType.MoveLocal, typeof(Transform), typeof(LocalPositionProvider));
            AddProvider(TweenType.Move, typeof(Transform), typeof(PositionProvider));
            AddProvider(TweenType.Move, typeof(Rigidbody), typeof(RigidbodyPositionProvider));

            AddProvider(TweenType.Scale, typeof(Transform), typeof(ScaleProvider));

            AddProvider(TweenType.Size2D, typeof(Camera), typeof(CameraRectSizeProvider));
            AddProvider(TweenType.Size2D, typeof(RectTransform), typeof(RectTransformSizeDeltaProvider));

            AddProvider(TweenType.Color, typeof (MeshFilter), typeof (MeshColorProvider));
            AddProvider(TweenType.Color, typeof(Renderer), typeof(RendererColorProvider));
            AddProvider(TweenType.Color, typeof(MaskableGraphic), typeof(GraphicColorProvider));

            AddProvider(TweenType.Rotation, typeof(Transform), typeof(TransformRotationProvider));
            AddProvider(TweenType.Rotation, typeof(Rigidbody), typeof(RigidbodyRotationProvider));

            AddProvider(TweenType.RotationLocal, typeof(Transform), typeof(TransformLocalRotationProvider));
            // TODO ? Ригидбоди

            AddProvider(TweenType.ShortestRotation, typeof(Transform), typeof(TransformShortestRotationProvider));
            AddProvider(TweenType.ShortestRotation, typeof(Rigidbody), typeof(RigidbodyShortestRotationProvider));

            AddProvider(TweenType.ShortestRotationLocal, typeof(Transform), typeof(TransformShortestRotationLocalProvider));

            AddProvider(TweenType.CameraOrthoSize, typeof(Camera), typeof(CameraOrthoSizeProvider));

        }

        /// <summary>
        /// Регистрация возможных вызовов методов. Необходимо для правильной работе проекста, собранного в Mono
        /// </summary>
        private void forAOTCompile()
        {
            GetProviders<float>(TweenType.Undefined, null);
            getProvider<float>(TweenType.Undefined, null);

            GetProviders<Vector2>(TweenType.Undefined, null);
            getProvider<Vector2>(TweenType.Undefined, null);

            GetProviders<Vector3>(TweenType.Undefined, null);
            getProvider< Vector3>(TweenType.Undefined, null);

            GetProviders<Color>(TweenType.Undefined, null);
            getProvider<Color>(TweenType.Undefined, null);

            GetProviders<Quaternion>(TweenType.Undefined, null);
            getProvider<Quaternion>(TweenType.Undefined, null);

        }


        public void AddProvider(TweenType tweenType, Type objectType, Type providerType)
        {
            if (!providerOptions.ContainsKey(tweenType))
                providerOptions[tweenType] = new List<KeyValuePair<Type, Type>>();
        
            providerOptions[tweenType].Add(new KeyValuePair<Type, Type>(objectType, providerType));
        }

        public List<IValueProvider<TValue>> GetProviders<TValue>(TweenType tweenType, object obj)
        {
            // Transform наследует IEnumerable
            if (obj is IValueProvider<TValue>)
                return new List<IValueProvider<TValue>> {(IValueProvider<TValue>) obj};

            List<IValueProvider<TValue>> providers = new List<IValueProvider<TValue>>(obj is ICollection ? ((ICollection)obj).Count : 1);

            IEnumerable objects = obj as IEnumerable;
            if (objects != null && !(obj is Component))
                foreach (object @object in objects)
                    providers.Add(getProvider<TValue>(tweenType, @object));
            else
                providers.Add(getProvider<TValue>(tweenType, obj));

            return providers;
        }

        private IValueProvider<TValue> getProvider<TValue>(TweenType tweenType, object obj)
        {
            if (obj is IValueProvider<TValue>)
                return (IValueProvider<TValue>)obj;

            GameObject go = obj as GameObject;

            if (go != null)
            {
                IValueProvider<TValue> result = go.GetComponent(typeof(IValueProvider<TValue>)) as IValueProvider<TValue>;
                if (result != null)
                    return result;
            }

            for (int i = providerOptions[tweenType].Count - 1; i > -1 ; i--)
            {
                KeyValuePair<Type, Type> kvp = providerOptions[tweenType][i];
                if (kvp.Key.IsInstanceOfType(obj))
                {
                    return (IValueProvider<TValue>) Activator.CreateInstance(kvp.Value, obj);
                }
                if (typeof(Component).IsAssignableFrom(kvp.Key))
                {
                    Component objFound;
                    
                    if (go != null && (objFound = go.GetComponent(kvp.Key)))
                        return (IValueProvider<TValue>) Activator.CreateInstance(kvp.Value, (object)objFound);

                    Component co = obj as Component;
                    if (co != null && ((objFound = co.GetComponent(kvp.Key))))
                        return (IValueProvider<TValue>)Activator.CreateInstance(kvp.Value, (object)objFound);
                }
            }

            Debug.LogError(string.Format("NotFound {1} provider for {0}({2})", tweenType, obj, obj.GetType()));

            return null;
        }

    

        #endregion
    }
}