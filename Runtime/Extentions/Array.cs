using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityUtility
{
    public static class ArrayExt
    {
        static Random random = new Random();

        public static T GetRandom<T>(this T[] array, out int index, int offset = 0)
        {
            var count = array?.Length;

            if (count == 0)
            {
                index = -1;
                return default;
            }
            else if (count == 1)
            {
                // TODO: ignoring offset ?
                index = 0;
                return array[0];
            }
            else
            {
                // TODO: check offset is in range 0..list.Count
                index = random.Next(offset, array.Length);
                return array[index];
            }
        }

        public static T GetRandom<T>(this T[] array, int offset = 0)
        {
            return GetRandom<T>(array, out var index, offset);
        }
    }
}