using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Bialik.Concurrent
{
    public class ConcurrentTwoWayMapping<TKey1, TKey2>
    {
        private ConcurrentDictionary<TKey1, ConcurrentHash<TKey2>> Key1ToKey2 { get; } =
            new ConcurrentDictionary<TKey1, ConcurrentHash<TKey2>>();
        private ConcurrentDictionary<TKey2, ConcurrentHash<TKey1>> Key2ToKey1 { get; } =
            new ConcurrentDictionary<TKey2, ConcurrentHash<TKey1>>();

        /// <summary>
        /// If TKey1 == TKey2 use 'TryRemoveBySecondKey'
        /// </summary>
        /// <param name="key2"></param>
        /// <returns></returns>
        public bool TryRemove(TKey2 key2) => TryRemoveBySecondKey(key2);

        public bool TryRemoveBySecondKey(TKey2 key2)
        {
            lock (this)
            {
                return Key2ToKey1.TryRemove(key2, out var relevantKey1S) &&
                       relevantKey1S.Aggregate(true,
                           (current, key1) =>
                               current && Key1ToKey2.TryGetValue(key1, out var d) &&
                               d.TryRemove(key2));
            }
        }

        /// <summary>
        /// If TKey1 == TKey2 use 'TryRemoveByFirstKey'
        /// </summary>
        /// <param name="key2"></param>
        /// <returns></returns>
        public bool TryRemove(TKey1 key1) => TryRemoveByFirstKey(key1);
        public bool TryRemoveByFirstKey(TKey1 key1)
        {
            lock (this)
            {
                return Key1ToKey2.TryRemove(key1, out var relevantKey2S) &&
                       relevantKey2S.Aggregate(true,
                           (current, key2) =>
                               current && Key2ToKey1.TryGetValue(key2, out var d) &&
                               d.TryRemove(key1));
            }
        }

        /// <summary>
        /// If TKey1 == TKey2 use 'TryGetValueByFirstKey'
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey1 key1, out IEnumerable<TKey2> result) => TryGetValueByFirstKey(key1, out result);
        public bool TryGetValueByFirstKey(TKey1 key1, out IEnumerable<TKey2> result)
        {
            if (!Key1ToKey2.TryGetValue(key1, out var res))
            {
                result = null;
                return false;
            }

            result = res;
            return true;
        }

        /// <summary>
        /// If TKey1 == TKey2 use 'TryGetValueBySecondKey'
        /// </summary>
        /// <param name="key2"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey2 key2, out IEnumerable<TKey1> result) => TryGetValueBySecondKey(key2, out result);
        public bool TryGetValueBySecondKey(TKey2 key2, out IEnumerable<TKey1> result)
        {
            if (!Key2ToKey1.TryGetValue(key2, out var res))
            {
                result = null;
                return false;
            }

            result = res;
            return true;
        }

        public void TryAdd(TKey1 key1, TKey2 key2)
        {
            lock (this)
            {
                Key1ToKey2.GetOrAdd(key1, new ConcurrentHash<TKey2>()).TryAdd(key2);
                Key2ToKey1.GetOrAdd(key2, new ConcurrentHash<TKey1>()).TryAdd(key1);
            }
        }

        public void TryAddReversed(TKey2 key2, TKey1 key1) => TryAdd(key1, key2);

    }
}