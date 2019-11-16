using System.Collections;
using Assets.Scripts.Core.TimeUtils;

namespace Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations
{
    public class TimeWaiterScenarioItem : ISyncScenarioItem, ISyncScenarioInterruptable
    {
	    public const float LessThanOneFrameTime = 0.0001f;

        private enum State
        {
            Undefined,
            Processing,
            Paused,
            Complete
        }
        #region Class fields
        private State state;
        private bool isInterruptable;
        private float timeout;
        private IEnumerator enumerator;
        private ITimeManager timeManager;
        #endregion


        #region Constructor
		public TimeWaiterScenarioItem(float timeout = LessThanOneFrameTime, bool isInterruptable = true)
            : this(timeout, isInterruptable, TimeManager.Instance)
        {
        }

        public TimeWaiterScenarioItem(float timeout, bool isInterruptable, ITimeManager timeManager)
        {
            this.timeout = timeout;
            this.isInterruptable = isInterruptable;
            this.timeManager = timeManager ?? TimeManager.Instance;
        }
        #endregion

        #region Override methods
        public bool IsComplete()
        {
            return this.state == State.Complete;
        }

        public void Play()
        {
            State stateWas = state;
            state = State.Processing;

            if (stateWas != State.Paused && stateWas != State.Processing)
                SyncCode.Instance.StartCoroutine(enumerator = timerCoroutine());
        }

        public void Stop()
        {
			if (enumerator != null && SyncCode.Instance != null)
            {
                SyncCode.Instance.StopCoroutine(enumerator);
            }

            state = State.Complete;
        }

        public void Pause()
        {
			if (state == State.Processing)
				state = State.Paused;
        }

        public bool IsCanBeInterrupted()
        {
            return isInterruptable;
        }
        #endregion

        #region Private methods

		private IEnumerator timerCoroutine()
        {
            float localTimeout = timeout;

            while (localTimeout > 0)
            {
                yield return null;

	            if (state == State.Processing)
	            {
		            localTimeout -= timeManager.GetDeltaTime();
		            process(localTimeout);
	            }
            }
            this.Stop();
        }

	    protected virtual void process(float localTimeout)
	    {
	    }

	    #endregion
    }
}