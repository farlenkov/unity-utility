#if UNITY_2017_1_OR_NEWER

using System.Collections.Generic;
using UnityEngine;

namespace UnityConfig
{
    public abstract class ListConfig<T> : ScriptableObject where T : ListConfig<T>
    {
        public static List<T> Load()
        {
            var configs = Resources.LoadAll<T>("");
            return new List<T>(configs);
        }
    }
}

#endif