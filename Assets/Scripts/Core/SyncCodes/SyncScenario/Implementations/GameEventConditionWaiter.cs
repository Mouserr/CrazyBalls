using System;
using System.Collections;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Core.TimeUtils;
using UnityEngine;

namespace Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations
{
    public class GameEventConditionWaiter<T> : ISyncScenarioItem where T : IGameEvent
    {
	    private readonly Func<T, bool> eventChecker;
	    protected readonly IGameEventListener listener;
        private readonly float? maxWaitTime;
        private bool complete;
        private bool isPaused;
        private float elapsedTime;
        private IEnumerator coroutine;
        private bool endedByTime;


        public GameEventConditionWaiter(Func<T, bool> eventChecker, float? maxWaitTime = null)
        {
	        this.eventChecker = eventChecker;
	        this.maxWaitTime = maxWaitTime;
	        listener = new GameEventListener<T>(onEvent);
        }

	    public bool EndedByTime
        {
            get { return endedByTime; }
        }

      

        protected void onEvent(T e)
        {
            if (eventChecker(e))
            {
                Stop();
            }
        }

        public bool IsComplete()
        {
            return complete;
        }

        public void Play()
        {
            listener.StartListening();
            if (isPaused)
            {
                isPaused = false;
            }
            else
            {
                if (maxWaitTime.HasValue)
                {
                    coroutine = playCoroutine(maxWaitTime.Value);
                    SyncCode.Instance.StartCoroutine(coroutine);
                }
            }

        }

        public void Stop()
        {
            complete = true;
            listener.StopListening();
            if (coroutine != null)
            {
                SyncCode.Instance.StopCoroutine(coroutine);
            }
        }

        public void Pause()
        {
            isPaused = true;
            listener.StopListening();
        }


        private IEnumerator playCoroutine(float secondsToWait)
        {
            while (true)
            {
                while (isPaused)
                    yield return null;

                elapsedTime += TimeManager.Instance.GetDeltaTime();
                if (elapsedTime < secondsToWait)
                {
                    yield return null;
                }
                else
                {
                    endedByTime = true;
                    Stop();
                    yield break;
                }
            }
        }
    }
}