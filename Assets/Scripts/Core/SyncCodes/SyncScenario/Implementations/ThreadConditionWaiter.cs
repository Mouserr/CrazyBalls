using System;
using System.Threading;

namespace Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations
{
	public class ThreadConditionWaiter : IConditionWaiter
	{
		private readonly Action actionToWait;
		private readonly Thread backgroundThead;
		private bool isTriggered;
		private bool isComplete = true;
		private readonly object locker = new object();

		public ThreadConditionWaiter(Action actionToWait)
		{
			this.actionToWait = actionToWait;
			backgroundThead = new Thread(threadAction);
		}

		public void Play()
		{
			lock (locker)
			{
				if (isComplete)
				{
					isTriggered = false;
					isComplete = false;
					backgroundThead.Start();
				}
			}
		}

		public void Stop()
		{
			lock (locker)
			{
				if (isComplete)
				{
					backgroundThead.Abort();

					complete(false);
				}
			}
		}

		private void complete(bool success)
		{
			isComplete = true;
			isTriggered = success;
		}

		public void Pause()
		{
		}

		private void threadAction()
		{
			actionToWait();

			lock (locker)
			{
				complete(true);
			}
		}

		public bool IsTriggered()
		{
			lock (locker)
			{
				return isTriggered;
			}
		}

		public bool IsComplete()
		{
			lock (locker)
			{
				return isComplete;
			}
		}
	}
}