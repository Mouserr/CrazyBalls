using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations
{
    public class CoroutineScenarioItem : ISyncScenarioItem, ISyncScenarioInterruptable
    {
        private enum State
        {
            Undefined,
            Playing,
            Pause,
            Complete
        }
        #region Class fields
        private readonly IEnumerator coroutine;
        private readonly bool isInterruptable;

        private State state = State.Undefined;
        private Coroutine wait;
        #endregion

        #region Constructor
        public CoroutineScenarioItem(IEnumerator coroutine, bool isInterruptable = true)
        {
            this.coroutine = coroutine;
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

            if (stateWas == State.Complete)
                coroutine.Reset();

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
