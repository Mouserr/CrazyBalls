using Assets.Scripts.Core.SyncCodes.SyncScenario;

namespace Assets.Scripts
{
    public static class SyncCodeExtensionsImpl
    {
        public static ISyncScenarioItem PlayRegisterAndReturnSelf(this ISyncScenarioItem item)
        {
            item.Play();
            MapController.Instance.AddScenario(item);
            return item;
        }
    }
}