using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtility
{
    public static class HashSetExt
    {
        public static void TryAdd<V>(this HashSet<V> list, V value)
        {
            if (!list.Contains(value))
                list.Add(value);
        }
        
        public static void TryRemove<V>(this HashSet<V> list, V value)
        {
            if (list.Contains(value))
                list.Remove(value);
        }
    }
}