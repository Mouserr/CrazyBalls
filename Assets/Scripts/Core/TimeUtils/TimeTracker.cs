using System.Collections;
using Assets.Scripts.Core.SyncCodes;
using UnityEngine;

namespace Assets.Scripts.Core.TimeUtils
{
	public class TimeTracker
	{
		private enum State
		{
			Undefined,
			Processing,
			Paused,
			Complete
		}
		private float duration;
		private State state;
		private IEnumerator enumerator;

		public void Start()
		{
			State stateWas = state;
            state = State.Processing;
			Debug.Log("Timer state switched to " + state);
			if (stateWas != State.Paused && stateWas != State.Processing)
			{
				duration = 0;
				SyncCode.Instance.StartCoroutine(enumerator = timerCoroutine());
			}
		}

		public float Stop()
		{
			if (enumerator != null && SyncCode.Instance != null)
			{
				SyncCode.Instance.StopCoroutine(enumerator);
			}

			state = State.Complete;
			Debug.Log("Timer state switched to " + state);
			return duration;
		}

		public float Pause()
		{
			if (state == State.Processing)
			{
				state = State.Paused;
				Debug.Log("Timer state switched to " + state);
			}

			return duration;
		}

		private IEnumerator timerCoroutine()
		{
			while (true)
			{
				yield return null;

				if (state == State.Processing)
				{
					duration += UnityEngine.Time.deltaTime;
				}
			}
		}
	}
}