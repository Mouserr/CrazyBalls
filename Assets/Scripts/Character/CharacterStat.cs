using System;

namespace Assets.Scripts
{
    [Serializable]
    public class CharacterStat
    {
        private CharacterStatType _type;
        private int _currentValue;
        private int _maxValue;

        public event Action<CharacterStat> Changed;
        public CharacterStatType Type => _type;

        public int MaxValue
        {
            get => _maxValue;
            set => _maxValue = value > 0 ? value : 0;
        }

        public int CurrentValue => _currentValue;

        public CharacterStat(CharacterStatType type, int maxValue)
        {
            _type = type;
            _currentValue = maxValue;
            _maxValue = maxValue;
        }

        public void AddValue(int value)
        {
            _currentValue += value;
            if (_currentValue > MaxValue) _currentValue = MaxValue;
            if (_currentValue < 0) _currentValue = 0;
            Changed?.Invoke(this);
        }

        public float Progress => CurrentValue / (float)MaxValue;
    }
}