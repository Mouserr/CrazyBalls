using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations
{
    public class ActionForeachScenarioItem<T> : ISyncScenarioItem
    {
        #region Class fields
        private readonly List<T> items;
        private readonly Action<T> action;

        private bool complete;
        #endregion


        #region Constructor
        public ActionForeachScenarioItem(List<T> items, Action<T> action)
        {
            this.items = items;
            this.action = action;
        }
        #endregion


        #region Override methods
        public bool IsComplete()
        {
            return complete;
        }

        public void Play()
        {
            foreach (T item in items)
            {
                action(item);
            }
            complete = true;
        }

        public void Stop()
        {
        }

        public void Pause()
        {
        }
        #endregion


        #region Private methods
        private static void MONO_SUPPORTING()
        {
            typeof(ActionForeachScenarioItem<GameObject>).ToString();
            typeof(ActionForeachScenarioItem<MonoBehaviour>).ToString();
        }
        #endregion

    }
}
