using System.Collections.Generic;
using Assets.Scripts.Core.TimeUtils;

namespace Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations
{
    public class CompositeItem : ISyncScenarioItem, ITimeDependent
    {
        #region Class fields
		protected IList<ISyncScenarioItem> items;
		private ITimeManager timeManager;
        #endregion

		#region Properies
		public ITimeManager TimeManager
		{
			get { return timeManager; }
			set
			{
				timeManager = value;

				for (int i = 0; i < items.Count; i++)
				{
					if (items[i] is ITimeDependent)
					{
						(items[i] as ITimeDependent).TimeManager = timeManager;
					}
				}
			}
		}
		#endregion

		#region Constructor
		public CompositeItem(IList<ISyncScenarioItem> items)
		{
			this.items = items;
			for (int i = 0; i < this.items.Count; i++)
			{
				if (this.items[i] == null)
				{
					this.items.RemoveAt(i);
					i--;
				}
			}
		}

		public CompositeItem(IEnumerable<ISyncScenarioItem> items)
			:this (new List<ISyncScenarioItem>(items))
        {
        }

        public CompositeItem(params ISyncScenarioItem[] items)
			: this(new List<ISyncScenarioItem>(items))
        {
        }
        #endregion

        #region Methods
        public virtual void Play()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (!items[i].IsComplete())
                    items[i].Play();
            }
        }

        public virtual void Stop()
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Stop();
            }
        }

        public virtual void Pause()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (!items[i].IsComplete())
                    items[i].Pause();
            }
        }

        public virtual bool IsComplete()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (!items[i].IsComplete())
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
