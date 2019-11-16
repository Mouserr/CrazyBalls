using System;

namespace Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations.Tween
{
    [Obsolete("Use TweenObject")]
    public abstract class TweenScenarioItem : ISyncScenarioItem, ISyncScenarioInterruptable
    {
        #region Class fields
        private readonly bool isInterruptable;

        private SimulateScenarioItem tweenObject;
        #endregion
    
        #region Constructor
        public TweenScenarioItem(bool isInterruptable)
        {
            this.isInterruptable = isInterruptable;
        }
        #endregion

        #region Properties
        public SimulateScenarioItem TweenObject
        {
            get { return this.tweenObject; }
        }
        #endregion

        #region Override methods
        public bool IsComplete()
        {
            return (null == tweenObject || tweenObject.IsComplete());
        }

        public void Play()
        {
            if (tweenObject != null)
            {
                if (!tweenObject.IsComplete())
                    tweenObject.Play();
            }
            else
                tweenObject = CreateTweenObject();
        }

        public void Stop()
        {
            if (!IsComplete())
            {
                tweenObject.Stop();
            }
        }

        public void Pause()
        {
            if (!IsComplete())
            {
                tweenObject.Pause();
            }
        }
    
        public bool IsCanBeInterrupted()
        {
            return isInterruptable;
        }
        #endregion

        #region Anbstract methods
        protected abstract SimulateScenarioItem CreateTweenObject();
        #endregion
    }
}
