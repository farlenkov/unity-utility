using System;
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

        public static bool TryGet<T>(this IList<T> list, int index, out T result)
        {
            if (list == null)
            {
                result = default;
                return false;
            }

            if (list.Count <= index)
            {
                result = default;
                return false;
            }

            var value = list[index];

            if (value is string valueStr &&
                string.IsNullOrEmpty(valueStr))
            {
                result = default;
                return false;
            }

            result = value;
            return true;
        }

        public static T Get<T>(this IList<T> list, int index)
        {
            list.TryGet(index, out var result);
            return result;
        }
    
        public static T Pool<T>(this IList<T> list, int index)
        {
            var item = list[index];
            list.RemoveAt(index);
            return item;
        }
    }
}