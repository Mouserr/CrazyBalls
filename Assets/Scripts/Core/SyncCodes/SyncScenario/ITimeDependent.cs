using Assets.Scripts.Core.TimeUtils;

namespace Assets.Scripts.Core.SyncCodes.SyncScenario
{
	public interface ITimeDependent
	{
		 ITimeManager TimeManager { get; set; }
	}
}