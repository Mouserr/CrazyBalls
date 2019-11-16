using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations
{
    public class LazyCoroutineScenarioItem : ISyncScenarioItem, ISyncScenarioInterruptable
    {
        private enum State
        {
            Undefined,
            Playing,
            Pause,
            Complete
        }
        #region Class fields
        private readonly Func<IEnumerator> func;
        private IEnumerator coroutine;
        private readonly bool isInterruptable;

        private State state = State.Undefined;
        private Coroutine wait;
        #endregion

        #region Constructor
        public LazyCoroutineScenarioItem(Func<IEnumerator> func, bool isInterruptable = true)
        {
            this.func = func;
            this.isInterruptable = isInterruptable;
        }
        #endregion

        #region Override methods
        public bool IsComplete()
        {
            return state == State.Complete;
        }

        public void Play()
        {
            State stateWas = state;

            state = State.Playing;

            if (stateWas == State.Pause)
                return;

            coroutine = (null != func) ? func() : null;
	        if (coroutine == null)
	        {
		        complete();
				return;
	        }
            wait = SyncCode.Instance.StartCoroutine(waiteCoroutine());
        }

        public void Stop()
        {
            if (wait == null)
                return;

            SyncCode.Instance.StopCoroutine(wait);
            complete();
        }


        public void Pause()
        {
            if (state == State.Playing)
                state = State.Pause;
        }

        public bool IsCanBeInterrupted()
        {
            return isInterruptable;
        }
        #endregion

        #region Private methods
        private IEnumerator waiteCoroutine()
        {
            while (true)
            {
                if (state == State.Pause)
                    yield return null;
                else
                {
                    if (!coroutine.MoveNext())
                        break;

                    yield return coroutine.Current;
                }
            }

            complete();
        }

        private void complete()
        {
            state = State.Complete;
            wait = null;
        }
        #endregion
    }
}
