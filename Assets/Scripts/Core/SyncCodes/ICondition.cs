using Assets.Scripts.Core.SyncCodes.SyncScenario;

namespace Assets.Scripts.Core.SyncCodes
{
    public interface ICondition
    {
        bool IsTriggered();
    }

	public class ReverseCondition : ICondition
	{
		private readonly ICondition condition;

		public ReverseCondition(ICondition condition)
		{
			this.condition = condition;
		}

		public virtual bool IsTriggered()
		{
			return !condition.IsTriggered();
		}
	}

    public interface IConditionWaiter : ISyncScenarioItem, ICondition
    {
    }
}