using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.SyncCodes
{
    
    class SafeMethodObject
    {
        #region Class fields
        private string id;
        private IEnumerator currentMethod;
        private IEnumerator nextMethod;
        #endregion

        #region Constructor
        public SafeMethodObject(string id, IEnumerator method)
        {
            this.id = id;
            this.currentMethod = method;
        }
        #endregion

        #region Properties
        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public IEnumerator ProcessMethod()
        {
            while (this.currentMethod != null)
            {
                yield return SyncCode.Instance.StartCoroutine(this.currentMethod);

                this.currentMethod = this.nextMethod != null ? this.nextMethod : null;
                this.nextMethod = null;
            }
        }

        public IEnumerator Method
        {
            get { return this.currentMethod; }
            set { this.nextMethod = value; }
        }
        #endregion
    }

    public class SyncCode : Singleton<SyncCode>
    {
        #region Constructor
        protected SyncCode() { }

        private Dictionary<string, SafeMethodObject> activeSafeMethods = new Dictionary<string, SafeMethodObject>();
        #endregion


        #region Public methods
        public Coroutine ExecuteMethodSafely(string id, IEnumerator method)
        {
            SafeMethodObject safeMethodObject = null;

            if (!this.activeSafeMethods.ContainsKey(id))
            {
                safeMethodObject = new SafeMethodObject(id, method);
                this.activeSafeMethods.Add(id, safeMethodObject);
                return StartCoroutine(this.processSafeMethod(id));
            }
            else
            {
                this.activeSafeMethods[id].Method = method;
                return null;
            }
        }

        public static SyncScenario.SyncScenario RunScenarioInstance(SyncScenario.SyncScenario previousInstance, Func<SyncScenario.SyncScenario> scenarioProvider)
        {
            if (null != previousInstance) previousInstance.Stop();

            var scenarioInstance = scenarioProvider();

            if (null != scenarioInstance) scenarioInstance.Play();

            return scenarioInstance;
        }
        #endregion


	    public Coroutine ExecuteCoroutineSequence(params Func<Coroutine>[] coroutineProviders)
	    {
			return StartCoroutine(enumerateCoroutines(coroutineProviders));
	    }

		private IEnumerator enumerateCoroutines(params Func<Coroutine>[] coroutineProviders)
	    {
			for (int i = 0; i < coroutineProviders.Length; i++)
			{
				yield return coroutineProviders[i].Invoke();
			}
	    }

        #region Private methods
        private IEnumerator processSafeMethod(string id)
        {
            yield return StartCoroutine(activeSafeMethods[id].ProcessMethod());

            activeSafeMethods.Remove(id);
        }
        #endregion
    }
}


