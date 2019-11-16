using UnityEngine;

namespace Assets.Scripts.Core.SyncCodes
{
    public class Callback
    {
        #region Class fields
        private GameObject callbackObject;
        private string callbackMethod;
        private System.Object param;

        private SendMessageOptions SEND_OPTIONS = SendMessageOptions.DontRequireReceiver;
        #endregion



        #region Constructors
        public Callback(GameObject callbackObject, string callbackMethod, System.Object param) : this(callbackObject, callbackMethod)
        {
            this.param = param;
        }

        public Callback(GameObject callbackObject, string callbackMethod)
        {
            this.callbackObject = callbackObject;
            this.callbackMethod = callbackMethod;
            param = null; 
        }
        #endregion


        #region Public methods
        public void Invoke(System.Object param)
        {
            this.param = param;
            Invoke();
        }

        public void Invoke()
        {
            if (callbackObject != null)
            {
                callbackObject.SendMessage(callbackMethod, param, SEND_OPTIONS);
            }
            else
            {
                Debug.LogError("Error! Callback.Invoke() : callbackObject is null");
            }
		    
        }

        public System.Object GetParam()
        {
            return param;
        }

        public void SetParam(System.Object param)
        {
            this.param = param;
        }

        public string GetCallbackMethodName() //for tests
        {
            return callbackMethod;
        }
        #endregion

    }
}

