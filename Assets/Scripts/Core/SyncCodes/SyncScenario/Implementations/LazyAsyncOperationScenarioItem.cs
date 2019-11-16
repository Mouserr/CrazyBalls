using System;

namespace Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations
{



    public class LazyAsyncOperationScenarioItem : ISyncScenarioItem
    {
        private bool started;
        private Func<ISyncOperation> operationProvider;
        private ISyncOperation operation;


        public LazyAsyncOperationScenarioItem(Func<ISyncOperation> operationProvider)
        {
            this.started = false;
            this.operationProvider = operationProvider;
            this.operation = null;
        }
        

        public void Play()
        {
            started = true;
            operation = operationProvider();
        }

        public void Stop()
        {
            //not supported
        }

        public void Pause()
        {
            //not supported
        }

        public bool IsComplete()
        {
            return started && (null == operation || operation.IsComplete());
        }
    }


}
