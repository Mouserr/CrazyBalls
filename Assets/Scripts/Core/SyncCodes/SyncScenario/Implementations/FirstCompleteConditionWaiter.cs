using System.Collections;

namespace Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations
{
	public class FirstCompleteConditionWaiter : IConditionWaiter
	{
		private readonly ICondition[] conditions;
		private bool complete;
		private bool isPaused;
		private IEnumerator playCoroutine;

		public FirstCompleteConditionWaiter(params ICondition[] conditions)
		{
			this.conditions = conditions;
		}

		public bool IsComplete()
		{
			return complete;
		}

		public void Play()
		{
			complete = false;
			isPaused = false;
			if (playCoroutine == null)
			{
				playCoroutine = waitCoroutine();
				for (int i = 0; i < conditions.Length; i++)
				{
					if (conditions[i] is ISyncScenarioItem)
					{
						(conditions[i] as ISyncScenarioItem).Play();
					}
				}
			}
			SyncCode.Instance.StartCoroutine(playCoroutine);
		}

		public void Stop()
		{
			Pause();
			complete = true;
			for (int i = 0; i < conditions.Length; i++)
			{
				if (conditions[i] is ISyncScenarioItem)
				{
					(conditions[i] as ISyncScenarioItem).Stop();
				}
			}
		}

		public void Pause()
		{
			isPaused = true;
			if (playCoroutine != null)
			{
				SyncCode.Instance.StopCoroutine(playCoroutine);
				playCoroutine = null;
				for (int i = 0; i < conditions.Length; i++)
				{
					if (conditions[i] is ISyncScenarioItem)
					{
						(conditions[i] as ISyncScenarioItem).Pause();
					}
				}
			}
		}

		public bool IsTriggered()
		{
			for (int i = 0; i < conditions.Length; i++)
			{
				if (conditions[i].IsTriggered())
					return true;
			}

			return false;
		}

		protected virtual IEnumerator waitCoroutine()
		{
			while (!isPaused)
			{
				if (IsTriggered())
				{
					Stop();
				}

				yield return null;
			}
		}
	}
}