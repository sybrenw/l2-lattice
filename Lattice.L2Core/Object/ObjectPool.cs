using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Lattice.L2Core.Object
{
    public class ObjectPool<T> where T : L2Object, new()
    {
        private readonly ConcurrentBag<T> _objects = new ConcurrentBag<T>();

        private int _currentId = 0;
        
        public T Get()
        {
            T obj; 
            if (_objects.TryTake(out obj))
                return obj;

            obj = new T() { Id = ++_currentId };
            return obj;
        }

        public void Release(T obj)
        {
            _objects.Add(obj);
        }

    }
}
