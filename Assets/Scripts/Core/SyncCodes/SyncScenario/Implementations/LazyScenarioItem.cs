using System;

namespace Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations
{




    public class LazyScenarioItem : ISyncScenarioItem
    {
        private bool started;
        private Func<ISyncScenarioItem> scenarioItemProvider;
        private ISyncScenarioItem scenarioItem;


        public LazyScenarioItem(Func<ISyncScenarioItem> scenarioItemProvider)
        {
            this.started = false;
            this.scenarioItemProvider = scenarioItemProvider;
            this.scenarioItem = null;
        }
        

        public void Play()
        {
            started = true;

            scenarioItem = scenarioItemProvider();
            if (null != scenarioItem)
            {
                scenarioItem.Play();
            }
        }

        public void Stop()
        {
            started = true;

            if (null != scenarioItem)
            {
                scenarioItem.Stop();
            }
        }

        public void Pause()
        {
            if (null != scenarioItem)
            {
                scenarioItem.Pause();
            }
        }

        public bool IsComplete()
        {
            return started && (null == scenarioItem || scenarioItem.IsComplete());
        }
    }



}
