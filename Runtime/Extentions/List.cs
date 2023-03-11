using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityUtility
{
    public static class ListExt
    {
        static Random random = new Random();

        public static T GetRandom<T>(this List<T> list, out int index, int offset = 0)
        {
            var count = list?.Count;

            if (count == 0)
            {
                index = -1;
                return default;
            }
            else if (count == 1)
            {
                // TODO: ignoring offset ?
                index = 0;
                return list[0];
            }
            else
            {                
                // TODO: check offset is in range 0..list.Count
                index = random.Next(offset, list.Count); 
                return list[index];
            }
        }

        public static T GetRandom<T>(this List<T> list, int offset = 0)
        {
            return GetRandom<T>(list, out var index, offset);
        }

        public static bool TryRemove<T>(this List<T> list, T item)
        {
            var index = list.IndexOf(item);

            if (index < 0)
                return false;

            list.RemoveAt(index);
            return true;
        }
    }
}