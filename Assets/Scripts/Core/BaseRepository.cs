using System;
using System.Collections.Generic;

namespace Assets.Scripts.Core
{
    public interface IRepository<KeyType, ValueType>
    {
        event EventHandler OnRepositoryChanged;
        long GetAmount(KeyType type);
        bool AddTransaction(KeyType type, ValueType value);
        void RegisterType(KeyType type);
        void UnRegisterType(KeyType type);
    }

    public class BaseRepository<T> : IRepository<T, long>
    {
        private readonly Dictionary<T, long> _repository;

        public BaseRepository()
        {
            _repository = new Dictionary<T, long>();
        }

        public event EventHandler OnRepositoryChanged;

        public long GetAmount(T type)
        {
            return _repository.ContainsKey(type) ? _repository[type] : 0;
        }

        public bool AddTransaction(T type, long value)
        {
            if (value == 0) return true;

            if (!_repository.ContainsKey(type))
            {
                if (value < 0) return false;
                RegisterType(type);
            }

            var result = _repository[type] + value;
            if (result < 0) return false;

            _repository[type] = result;
            OnRepositoryChanged?.Invoke(this, null);
            return true;
        }

        public void RegisterType(T type)
        {
            if (!_repository.ContainsKey(type)) _repository.Add(type, 0);
        }

        public void UnRegisterType(T type)
        {
            if (_repository.ContainsKey(type)) _repository.Remove(type);
        }
    }
}