#if UNITY_2017_1_OR_NEWER

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtility
{
    public static class RaycastHitExt
    {
        static RaycastHitDistanceComparer distanceComparer = new RaycastHitDistanceComparer();

        public static void Sort(
            this RaycastHit[] hits,
            int hitsCount)
        {
            Array.Sort(hits, 0, hitsCount, distanceComparer);
        }

        class RaycastHitDistanceComparer : IComparer<RaycastHit>
        {
            public int Compare(RaycastHit x, RaycastHit y)
            {
                return x.distance.CompareTo(y.distance);
            }
        }
    }
}

#endif