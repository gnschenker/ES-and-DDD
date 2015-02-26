using System;
using System.Collections.Generic;

namespace TaskManager.Infrastructure
{
    internal interface IIdGenerator
    {
        int Next<T>();
    }

    public class IdGenerator : IIdGenerator
    {
        static readonly Dictionary<Type, int> _cache = new Dictionary<Type, int>(); 
        
        public int Next<T>()
        {
            var type = typeof (T);
            if (_cache.ContainsKey(type) == false)
                _cache[type] = 1;
            return _cache[type]++;
        }
    }
}