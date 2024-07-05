using System;
using System.Collections.Generic;

namespace UnityUtility
{
    public class IndexSet<K, V>
	{
        Dictionary<K, V> index = new ();
        List<V> list = new ();

        public IReadOnlyDictionary<K, V> Index => index;
        public IReadOnlyList<V> List => list;

        public int Count => index.Count;

        public bool ContainsKey(K key) => index.ContainsKey(key);

        public bool ContainsValue(V value) => index.ContainsValue(value);

        public bool TryGetValue(K key, out V value) => index.TryGetValue(key, out value);

        public virtual void Add (K key, V value)
        {
            index.Add(key, value);
            list.Add(value);
        }

        public void SetItem(K key, V value)
        {
            if (index.ContainsKey(key))
                Replace(key, value);
            else
                Add(key, value);
        }

        public virtual V Remove (K key)
        {
            if (index.TryGetValue(key, out var value))
            {
                index.Remove(key);
                list.Remove(value);
                return value;
            }

            return default(V);
        }

        public virtual V Replace(K key, V newItem)
        {
            if (index.TryGetValue(key, out var oldItem))
            {
                var index = list.IndexOf(oldItem);

                this.index[key] = newItem;
                list[index] = newItem;

                return oldItem;
            }

            return default(V);
        }

        public virtual void Clear()
        {
            index.Clear();
            list.Clear();
        }

        public void Sort(Comparison<V> callback)
        {
            list.Sort(callback);
        }
    }
}