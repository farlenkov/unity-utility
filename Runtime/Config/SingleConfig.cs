#if UNITY_2017_1_OR_NEWER

using UnityEngine;

namespace UnityConfig
{
    public abstract class SingleConfig<T> 
        : ScriptableObject where T : SingleConfig<T>
    {
        static T instance;
        
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    var all = Resources.LoadAll<T>(string.Empty);

                    if (all.Length > 0)
                        instance = all[0];
                }

                return instance;
            }
        }
    }
}

#endif
