namespace Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations
{



    public class AsyncOperationScenarioItem : ISyncScenarioItem
    {
        private bool started;
        private ISyncOperation syncOperation;


        public AsyncOperationScenarioItem(ISyncOperation syncOperation)
        {
            this.started = false;
            this.syncOperation = syncOperation;
        }
        

        public void Play()
        {
            started = true;
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
            return started && (null == syncOperation || syncOperation.IsComplete());
        }
    }


}
