using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Core.TimeUtils;
using UnityEngine;

namespace Assets.Scripts.Core.SyncCodes.SyncScenario
{
    public class SyncScenario : ISyncScenarioItem, ISyncScenarioInterruptable, ITimeDependent
    {
        #region Class fields
        private IList<ISyncScenarioItem> scenarioItems;
        private bool shouldBeInterropted;
	    private ITimeManager timeManager;

        private Callback callBack;
        private Action<SyncScenario, bool> callBackAction;

        private int activeItem;
        private IEnumerator processScenarioInvoke;
        private bool started;


        private IScenarioContext context = null;
        #endregion

        #region Constructor
        public SyncScenario()
        {
            Initialize(null, null);
        }

        public SyncScenario(params ISyncScenarioItem[] scenarioItems)
        {
            Initialize(new List<ISyncScenarioItem>(scenarioItems));
        }

        public SyncScenario(IList<ISyncScenarioItem> scenarioItems, Callback callBack = null)
        {
            Initialize(scenarioItems, callBack);
        }

        public SyncScenario(IList<ISyncScenarioItem> scenarioItems, Action<SyncScenario, bool> callBackAction)
        {
            Initialize(scenarioItems, null, callBackAction);
        }

        public SyncScenario(IList<ISyncScenarioItem> scenarioItems, Callback callBack, Action<SyncScenario, bool> callBackAction)
        {
            Initialize(scenarioItems, callBack, callBackAction);
        }
        #endregion

        #region Properties
        public bool IsPlaying
        {
            get { return !IsComplete(); }
        }

        public bool IsPause
        {
            get; protected set;
        }

        public bool Started
        {
            get { return started; }
        }

        public IScenarioContext Context
        {
            get { return context; }
        }

	    public ITimeManager TimeManager
	    {
		    get { return timeManager; }
		    set
		    {
			    timeManager = value;

			    for (int i = 0; i < scenarioItems.Count; i++)
			    {
				    if (scenarioItems[i] is ITimeDependent)
				    {
					    (scenarioItems[i] as ITimeDependent).TimeManager = timeManager;
				    }
			    }
		    }
	    }
        #endregion

        #region Methods
        public SyncScenario Clone()
        {
            return new SyncScenario(scenarioItems, callBack);
        }

        public virtual void Play()
        {
            Play((IScenarioContext)null);
        }

        public virtual void Play(IScenarioContext context)
        {
            this.context = context;
            
            if (IsPause)
            {
                IsPause = false;

                if (scenarioItems[activeItem] != null)
                {
                    IContextableScenarioItem contextableItem = scenarioItems[activeItem] as IContextableScenarioItem;
                    if (contextableItem == null) scenarioItems[activeItem].Play();
                    else contextableItem.Play(context);
                }
            }
            else
            {
                IsPause = false;

                shouldBeInterropted = false;
                processScenarioInvoke = ProcessScenario();
                SyncCode.Instance.StartCoroutine(processScenarioInvoke);    
            }
        }

        public virtual void Pause()
        {
            if (IsPlaying)
            {
                IsPause = true;
                if (scenarioItems[activeItem] != null) scenarioItems[activeItem].Pause();    
            }
        }

        public virtual void Interrupt()
        {
            if (IsPlaying && !shouldBeInterropted)
            {
                shouldBeInterropted = true;
                SyncCode.Instance.StartCoroutine(ProcessInterruption());
            }
        }

        private IEnumerator ProcessInterruption()
        {
            while (!isActiveItemCanBeInterrupted()) yield return null;
            Stop();
        }

        public virtual void Stop()
        {
            foreach (var scenarioItem in scenarioItems)
            {
                if (null != scenarioItem && !scenarioItem.IsComplete()) scenarioItem.Stop();
            }

            if (null != processScenarioInvoke && null != SyncCode.Instance)
            {
                SyncCode.Instance.StopCoroutine(processScenarioInvoke);
                processScenarioInvoke = null;
            }
        
            onCameToEnd(true);
        }

        private IEnumerator ProcessScenario()
        {
            started = true;

            for (activeItem = 0; activeItem < scenarioItems.Count; activeItem++)
            {
                if (scenarioItems[activeItem] != null)
                {
                    IContextableScenarioItem contextableItem = scenarioItems[activeItem] as IContextableScenarioItem;
                    if (null == contextableItem) scenarioItems[activeItem].Play();
                    else contextableItem.Play(context);

                    if (IsPause)
                    {
                        scenarioItems[activeItem].Pause();
                    }

					if (!scenarioItems[activeItem].IsComplete())
					{
						yield return SyncCode.Instance.WaitOperations(scenarioItems[activeItem]);
					}
                }
                else
                {
                    Debug.LogWarning("Null item in scenario");
                    if(callBack != null)
                    {
                        Debug.LogWarning("Callback is " + callBack.GetCallbackMethodName());
                    }
                }
	            
				while (IsPause)
                    yield return null;

                if (IsComplete()) break;
            }
            activeItem = 0;

            onCameToEnd(false);
            processScenarioInvoke = null;
        }

        private void onCameToEnd(bool force)
        {
            shouldBeInterropted = true;
            IsPause = false;
            fireCallBack(force);
			onComplete();
        }

        private void fireCallBack(bool force)
        {
            if (callBack != null)
            {
                callBack.Invoke(force);
				callBack = null;
			}
            if (callBackAction != null)
            {
                callBackAction.Invoke(this, force);
				callBackAction = null;
			}
            onComplete();
        }

        protected virtual void onComplete()
        {
        }

        public virtual bool IsCanBeInterrupted()
        {
            return true;
        }

        public bool IsComplete()
        {
            foreach (var scenarioItem in scenarioItems)
            {
                if (null != scenarioItem && !scenarioItem.IsComplete()) return false;
            }
            return true;
        }

        private bool isActiveItemCanBeInterrupted()
        {
            ISyncScenarioInterruptable interruptableItem = (0 == scenarioItems.Count)? null : (scenarioItems[activeItem] as ISyncScenarioInterruptable);
            return null == interruptableItem || interruptableItem.IsCanBeInterrupted();
        }

        protected void Initialize(IList<ISyncScenarioItem> scenarioItems, Callback callBack = null, Action<SyncScenario, bool> callBackAction = null)
        {
            if (null == scenarioItems)
            {
                scenarioItems = new List<ISyncScenarioItem>();
            }
            this.scenarioItems = scenarioItems;
            this.shouldBeInterropted = false;
            this.callBack = callBack;
            this.callBackAction = callBackAction;
            this.activeItem = 0;
            this.started = false;
        }

        public void Print()
        {
        }
        #endregion





        #region Static utils
        public static SyncScenario Play(SyncScenario prviousInstance, Func<SyncScenario> scenarioProvider)
        {
            Stop(prviousInstance);
            return Play(scenarioProvider());
        }

        public static SyncScenario Play(SyncScenario scenario)
        {
            if (null != scenario)
            {
                scenario.Play();
            }
            return scenario;
        }

        public static bool SetPause(SyncScenario scenario, bool state)
        {
            if (state)
            {
                if (!IsComplete(scenario) && !IsPaused(scenario))
                {
                    scenario.Pause();
                    return true;
                }
            }
            else
            {
                if (!IsComplete(scenario) && IsPaused(scenario))
                {
                    scenario.Play();
                    return true;
                }
            }
            return false;
        }

        public static void Stop(SyncScenario scenario)
        {
            if (null != scenario) scenario.Stop();
        }
      
        public static bool IsNotStarted(SyncScenario scenario)
        {
            return (null != scenario) && !scenario.Started;
        }

        public static bool IsComplete(SyncScenario scenario)
        {
            return (null == scenario) || scenario.IsComplete();
        }

        public static bool IsPaused(SyncScenario scenario)
        {
            return (null != scenario) && scenario.IsPause;
        }
        #endregion

	   
    }
}
