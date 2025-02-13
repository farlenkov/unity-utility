#if UNITY_2017_1_OR_NEWER

using CoreUtils;
using UnityEngine;

namespace UnityConfig
{
    public abstract class SingleConfig<T> : ScriptableObject where T : SingleConfig<T>
    {
        public static T Load()
        {
            var configs = Resources.LoadAll<T>("");

            if (configs.Length == 0)
                return null;

            if (configs.Length > 1)
                Log.WarningEditor($"[{typeof(T).Name}: Load] More than one config found");

            return configs[0];
        }
    }
}

#endif
