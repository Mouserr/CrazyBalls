using System.Collections.Generic;
using System.Collections.ObjectModel;
using Assets.Scripts.Core.Tween.TweenSimulators.SimulateFunctions;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;

namespace Assets.Scripts.Core.Tween.TweenSimulators
{
    public abstract class TweenSimulator<TValue> : ISimulateable
    {
        #region Class fields
        protected readonly ReadOnlyCollection<IValueProvider<TValue>> providers;
        protected readonly TValue[] shifts;
        protected readonly TValue[] previosValue;
        protected readonly float duration;
        private readonly ISimulateFunction function;

        protected readonly TValue endValue;
        protected readonly TweenEndValueType tweenEndValueType;
        #endregion

        #region Constructor
        protected TweenSimulator(IList<IValueProvider<TValue>> providers, TValue endValue, float duration, ISimulateFunction function, TweenEndValueType tweenEndValueType)
        {
            this.providers = new ReadOnlyCollection<IValueProvider<TValue>>(providers);
            this.endValue = endValue;
            this.duration = duration;
            this.function = function;
            this.tweenEndValueType = tweenEndValueType;

            this.shifts = new TValue[this.providers.Count];
            this.previosValue = new TValue[this.providers.Count];
        }

        public ISimulateFunction SimulateFunction
        {
            get { return function; }
        }

        #endregion

        #region Methods
        public abstract void Simulate(float time);

	    public virtual void Init()
	    {
		    for (int i = 0; i < providers.Count; i++)
		    {
			    IInitializable initializable = providers[i] as IInitializable;

			    if (initializable != null)
			    {
				    initializable.Init();
			    }
		    }
	    }
        #endregion
    
        protected static List<IValueProvider<TValue>> GetProviders(object obj, TweenType tweenType)
        {
            return ValueProviderBuilder.Instance.GetProviders<TValue>(tweenType, obj);
        }
    }
}
