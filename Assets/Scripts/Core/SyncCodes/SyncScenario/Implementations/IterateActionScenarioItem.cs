using System;
using Assets.Scripts.Core.TimeUtils;

namespace Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations
{
	public class IterateActionScenarioItem : TimeWaiterScenarioItem
	{
		private readonly Action<float> action;

		public IterateActionScenarioItem(Action<float> action, float timeout, bool isInterruptable = true)
			: this(action, timeout, isInterruptable, TimeManager.Instance)
		{
		}

		public IterateActionScenarioItem(Action<float> action, float timeout, bool isInterruptable, ITimeManager timeManager)
			: base(timeout, isInterruptable, timeManager)
		{
			this.action = action;
		}

		protected override void process(float localTimeout)
		{
			base.process(localTimeout);
			action.Invoke(localTimeout);
		}
	}
}