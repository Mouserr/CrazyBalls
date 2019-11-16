using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.Helpers
{
    public class ComponentHelper
    {
        public static List<GameObject> GetGameObjects<T>(List<T> monoBehaviours) where T : Component
        {
            List<GameObject> gameObjects = new List<GameObject>();
            foreach (T component in monoBehaviours)
                gameObjects.Add(component.gameObject);

            return gameObjects;
        }

        public static TComponent GetComponent<TComponent>(object obj)
            where TComponent : Component
        {
            TComponent component = obj as TComponent;
            if (component == null)
            {
                GameObject go = obj as GameObject;
                if (go != null)
                    component = go.GetComponent<TComponent>();
                else
                {
                    Component co = obj as Component;
                    if (co != null)
                        component = co.GetComponent<TComponent>();
                }
            }
            if (component == null)
                Debug.LogErrorFormat("{0} not found", typeof(TComponent));

            return component;
        }
    }
}
