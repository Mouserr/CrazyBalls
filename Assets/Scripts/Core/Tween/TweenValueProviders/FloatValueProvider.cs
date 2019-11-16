using System;
using Assets.Scripts.Core.Tween.TweenValueProviders.Base;

namespace Assets.Scripts.Core.Tween.TweenValueProviders
{



    public class FloatValueProvider : IValueProvider<float>
    {
        #region Class fields
        private Action<float> valueSetter;
        private Func<float> valueGetter;
        #endregion

        #region Properties
        public float Value { get; set; }
        #endregion

        #region Constructor
        public FloatValueProvider(Func<float> valueGetter, Action<float> valueSetter)
        {
            this.valueGetter = valueGetter;
            this.valueSetter = valueSetter;
        }
        #endregion

    }
}
