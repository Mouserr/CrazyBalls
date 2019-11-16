using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations
{
	public class SimpleConditionWaiter : IConditionWaiter
	{
		private readonly Func<bool> conditionChecker;
		private readonly ICondition condition;
		private readonly float checkIntervalSec;
		private bool complete;
		private bool isPaused;
		private IEnumerator playCoroutine;

		public SimpleConditionWaiter(ICondition condition, float checkIntervalSec = 0)
		{
			this.condition = condition;
			this.checkIntervalSec = checkIntervalSec;
		}

		public SimpleConditionWaiter(Func<bool> conditionChecker, float checkIntervalSec = 0)
		{
			this.conditionChecker = conditionChecker;
			this.checkIntervalSec = checkIntervalSec;
		}

		public bool IsComplete()
		{
			return complete;
		}

		public virtual void Play()
		{
			complete = false;
			isPaused = false;
			if (playCoroutine == null)
			{
				playCoroutine = waitCoroutine();
			}
			SyncCode.Instance.StartCoroutine(playCoroutine);
		}

		public virtual void Stop()
		{
			Pause();
			complete = true;
		}

		public void Pause()
		{
			isPaused = true;
			if (playCoroutine != null)
			{
				SyncCode.Instance.StopCoroutine(playCoroutine);
				playCoroutine = null;
			}
		}

		public virtual bool IsTriggered()
		{
			return condition != null && condition.IsTriggered()
				|| conditionChecker != null && conditionChecker();
		}

		protected virtual IEnumerator waitCoroutine()
		{
			while (!isPaused)
			{
				if (IsTriggered())
				{
					Stop();
				}

				if (checkIntervalSec <= 0)
				{
					yield return null;
				}
				else
				{
					yield return new WaitForSeconds(checkIntervalSec);
				}
			}
		}
	}
}