using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Core.Events;
using Assets.Scripts.Core.TimeUtils;

namespace Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations
{
    public class EventWaiter : ISyncScenarioItem
    {
        protected readonly List<IGameEventListener> listeners;
        private readonly int eventsToStopCount;
        private readonly float? maxWaitTime;
        private int currentCount;
        private bool complete;
        private bool isPaused;
        private float elapsedTime;
        private IEnumerator coroutine;
        private bool endedByTime;


        public EventWaiter(int eventsToStopCount, float? maxWaitTime = null)
        {
            listeners = new List<IGameEventListener>();
            this.eventsToStopCount = eventsToStopCount;
            this.maxWaitTime = maxWaitTime;
            currentCount = 0;
        }

        public bool EndedByTime
        {
            get { return endedByTime; }
        }

        public void AddGlobalEvent<T>() where T : IGameEvent
        {
            var listener = new GameEventListener<T>(onEvent);
            listeners.Add(listener);
        }

        protected void onEvent<T>(T e) where T : IGameEvent
        {
            currentCount++;
            if (currentCount >= eventsToStopCount)
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
            foreach (var listener in listeners)
            {
                listener.StartListening();
            }
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
            foreach (var listener in listeners)
            {
                listener.StopListening();
            }
            listeners.Clear();
            if (coroutine != null)
            {
                SyncCode.Instance.StopCoroutine(coroutine);
            }
        }

        public void Pause()
        {
            isPaused = true;
            foreach (var listener in listeners)
            {
                listener.StopListening();
            }
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