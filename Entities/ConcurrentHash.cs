using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Bialik.Concurrent
{
    public class ConcurrentHash<T> : IEnumerable<T>
    {
        private readonly ConcurrentDictionary<T, bool> _inner = new ConcurrentDictionary<T, bool>();

        public bool TryAdd(T v)
        {
            return _inner.TryAdd(v, true);
        }

        public bool TryRemove(T v) => _inner.TryRemove(v, out _);

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => _inner.Keys.GetEnumerator();

        public IEnumerator GetEnumerator() => _inner.Keys.GetEnumerator();

        public T[] ToArray => _inner.Keys.ToArray();

    }
}
