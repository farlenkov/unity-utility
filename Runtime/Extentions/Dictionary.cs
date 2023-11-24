using System.Collections;
using System.Collections.Generic;

namespace UnityUtility
{
    public static class DictionaryExt
    {
        // GET / SET / REMOVE

        public static V GetItem<K, V>(
            this Dictionary<K, V> dict,
            K key,
            bool createNew = true)
            where V : new()
        {
            if (!dict.TryGetValue(key, out V item) && createNew)
            {
                item = new V();
                dict.Add(key, item);
            }

            return item;
        }

        public static void SetItem<K, V>(this Dictionary<K, V> dict, K key, V value)
        {
            if (dict.ContainsKey(key))
                dict[key] = value;
            else
                dict.Add(key, value);
        }

        public static void RemoveItem<K, V>(this Dictionary<K, V> dict, K key)
        {
            if (dict.ContainsKey(key))
                dict.Remove(key);
        }

        public static void Reset<K, V>(ref Dictionary<K, V> dict)
        {
            if (dict == null)
                dict = new Dictionary<K, V>();
            else
                dict.Clear();
        }

        // CHANGE VALUE

        public static int ChangeItem<K>(this Dictionary<K, int> dict, K key, int changeValue)
        {
            if (dict.TryGetValue(key, out var value))
            {
                var newValue = value + changeValue;
                dict[key] = newValue;
                return newValue;
            }
            else
            {
                dict.Add(key, changeValue);
                return changeValue;
            }
        }
    }
}