using System.Collections;
using System.Collections.Generic;

namespace UnityUtility
{
    public static class DictionaryExt
    {
        public static V GetItem<K, V>(
            this Dictionary<K, V> dict, 
            K key,
            bool create_new = true) 
            where V : new()
        {
            if (!dict.TryGetValue(key, out V item) && create_new)
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
    }
}