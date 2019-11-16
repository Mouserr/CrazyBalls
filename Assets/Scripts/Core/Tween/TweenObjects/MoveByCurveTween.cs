using Assets.Scripts.Core.Curves;
using Assets.Scripts.Core.SyncCodes;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using Assets.Scripts.Core.Tween.TweenSimulators;
using Assets.Scripts.Core.Tween.TweenSimulators.SimulateFunctions;

namespace Assets.Scripts.Core.Tween.TweenObjects
{
    public class MoveByCurveTween : SimulateScenarioItem
    {
        #region Constructor
        public  MoveByCurveTween(AbstractCurve curve, object obj, float duration, EaseType easeType, TweenSpace space = TweenSpace.Global, Callback callback = null)
            : this(curve, obj, duration, new EaseSimulateFunction(TweenPerformer.Ease[easeType]), space, callback)
        {
        }

        public MoveByCurveTween(AbstractCurve curve, object obj, float duration, ISimulateFunction function, TweenSpace space = TweenSpace.Global, Callback callback = null)
            : base(new CurveTweenSimulator(curve, obj, duration, function, TweenPerformer.GetShiftTypeBySpace(space)), duration, null, callback)
        {
        }
        #endregion


        #region Static methods
        public static MoveByCurveTween Play(AbstractCurve curve, object obj, float duration, EaseType easeType, TweenSpace space = TweenSpace.Global)
        {
            return (MoveByCurveTween)(new MoveByCurveTween(curve, obj, duration, new EaseSimulateFunction(TweenPerformer.Ease[easeType]), space)).PlayAndReturnSelf();
        }
        #endregion
    }
}
