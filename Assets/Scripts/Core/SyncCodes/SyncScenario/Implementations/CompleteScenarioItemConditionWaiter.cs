using System;
using System.Collections;

namespace Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations
{
	public class CompleteScenarioItemConditionWaiter : IConditionWaiter
	{
		private readonly Func<ISyncScenarioItem> syncScenarioItemGetter;
		private ISyncScenarioItem syncScenarioItem;
		private readonly bool isStarted;
		private bool complete;
		private bool isPaused;
		private IEnumerator playCoroutine;

		public CompleteScenarioItemConditionWaiter(ISyncScenarioItem syncScenarioItem, bool isStarted = false)
		{
			this.syncScenarioItem = syncScenarioItem;
			this.isStarted = isStarted;
		}

		public CompleteScenarioItemConditionWaiter(Func<ISyncScenarioItem> syncScenarioItemGetter)
		{
			this.syncScenarioItemGetter = syncScenarioItemGetter;
		}

		public bool IsComplete()
		{
			return complete;
		}

		public virtual void Play()
		{
			complete = false;
			isPaused = false;
			if (syncScenarioItem == null)
			{
				if (syncScenarioItemGetter == null)
				{
					Stop();
					return;
				}

				syncScenarioItem = syncScenarioItemGetter();
			}
			if (!isStarted)
				syncScenarioItem.Play();
			if (playCoroutine == null)
			{
				playCoroutine = waitCoroutine();
			}
			SyncCode.Instance.StartCoroutine(playCoroutine);
		}

		public virtual void Stop()
		{
			if (syncScenarioItem != null && !syncScenarioItem.IsComplete())
			{
				Pause();
				syncScenarioItem.Stop();
			}
			complete = true;
		}

		public void Pause()
		{
			isPaused = true;
			syncScenarioItem.Pause();
			if (playCoroutine != null)
			{
				SyncCode.Instance.StopCoroutine(playCoroutine);
				playCoroutine = null;
			}
		}

		public virtual bool IsTriggered()
		{
			return syncScenarioItem.IsComplete();
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