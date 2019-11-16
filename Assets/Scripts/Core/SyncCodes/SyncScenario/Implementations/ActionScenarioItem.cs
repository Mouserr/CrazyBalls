using System;

namespace Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations
{
    public class ActionScenarioItem : ISyncScenarioItem
    {
        #region Class fields
        private readonly Action action;

        private bool complete;
        #endregion

        #region Constructor
        public ActionScenarioItem(Action action)
        {
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
            action();
            complete = true;
        }

        public void Stop()
        {
            complete = true;
        }

        public void Pause()
        {
     //       ExceptionHelper.MaybeThrow<ActionScenarioItem>(new NotSupportedException());
        }
        #endregion

    }
}
