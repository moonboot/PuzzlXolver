using System;
using System.Collections.Generic;

namespace PuzzlXolver
{
    public static class DictionaryExtensions
    {
        public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TValue> createValue)
        {
            TValue value;
            if (dictionary.TryGetValue(key, out value))
            {
                return value;
            }
            else {
                value = createValue();
                dictionary[key] = value;
                return value;
            }
        }
    }
}
