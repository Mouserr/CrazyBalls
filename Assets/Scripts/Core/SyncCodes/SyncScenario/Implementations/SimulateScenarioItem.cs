using System.Collections;
using Assets.Scripts.Core.TimeUtils;
using Assets.Scripts.Core.Tween.TweenSimulators;


namespace Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations
{
    public class SimulateScenarioItem : ISyncScenarioItem, ITimeDependent
    {
        #region Class fields
        private readonly ISimulateable simulator;
        private readonly float duration;
        private readonly Callback callback;
        private ITimeManager timeManager;

        private float curTime;
    
        private IEnumerator simulateCorutine;
        private State state;

        protected enum State
        {
            Undefined,
            Active,
            Paused,
            Stopped
        }
        #endregion

        #region Constructor
        public SimulateScenarioItem(ISimulateable simulator, float duration, ITimeManager timeManager = null, Callback callback = null)
        {
            this.simulator = simulator;
            this.duration = duration;
            this.callback = callback;
            this.timeManager = timeManager ?? TimeUtils.TimeManager.Instance;
        }
        #endregion

        #region Properties
        public ISimulateable Simulator 
        { 
            get { return this.simulator; }
        }

        public float CurTime 
        { 
            get { return this.curTime; }
            protected set
            {
                this.curTime = value < 0.0f ? 0.0f : value > this.duration ? this.duration : value;
            } 
        }

        public float Duration 
        { 
            get { return this.duration; } 
        }

        public Callback Callback 
        { 
            get { return this.callback; } 
        }

        public ITimeManager TimeManager
        {
            get { return timeManager; }
            set { timeManager = value; }
        }
        #endregion

        #region Method overrides
        public virtual void Play()
        {
            state = State.Active;

            if (this.simulateCorutine == null)
            {
                this.simulateCorutine = Simulate();
                SyncCode.Instance.StartCoroutine(this.simulateCorutine);
            }
        }

        public virtual void Pause()
        {
            state = State.Paused;
        }

        public virtual void Stop()
        {
            state = State.Stopped;
            if (this.simulateCorutine == null || SyncCode.Instance == null) 
                return;

            SyncCode.Instance.StopCoroutine(this.simulateCorutine);
            this.simulateCorutine = null;

            if (null != this.callback) this.callback.Invoke();
        }

        protected virtual IEnumerator Simulate()
        {
            CurTime = 0.0f;
            simulator.Init();

            while (state != State.Stopped)
            {
                if (state != State.Paused)
                {
                    CurTime += timeManager.GetDeltaTime();
                    
                    if (CurTime < Duration)
                    {
                        simulator.Simulate(CurTime);
                    }
                    else
                    {
                        simulator.Simulate(Duration);
                        Stop();
                    }
                }
                yield return null;
            }
        }

        public bool IsComplete()
        {
            return state == State.Stopped;
        }
        #endregion
    }
}
