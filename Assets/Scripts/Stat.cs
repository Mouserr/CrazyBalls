using System;

namespace Assets.Scripts
{
    public class Stat
    {
        private int _currentValue;
        public int MaxValue;

        public event Action<Stat> Changed;

        public int CurrentValue
        {
            get { return _currentValue; }
            set
            {
                if (_currentValue != value)
                {
                    _currentValue = value;
                    Changed?.Invoke(this);
                }
            }
        }

        public float Progress => CurrentValue / (float)MaxValue;
    }
}