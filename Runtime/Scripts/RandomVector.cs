using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtility
{

#if UNITY_2017_1_OR_NEWER

    public static class RandomVector
    {
        public static Vector3 Range (Vector3 min, Vector3 max)
        {
            return new Vector3(
                Random.Range(min.x, max.x),
                Random.Range(min.y, max.y),
                Random.Range(min.z, max.z));
        }
    }

#endif

}