#if UNITY_2017_1_OR_NEWER

using System.Collections.Generic;
using UnityEngine;

namespace UnityConfig
{
    public abstract class ListConfig<T> 
        : ScriptableObject where T : ListConfig<T>
    {
        static List<T> all;

        public static List<T> All
        {
            get
            {
                if (all == null)
                    all = new List<T>(Resources.LoadAll<T>(string.Empty));

                return all;
            }
        }
    }
}

#endif