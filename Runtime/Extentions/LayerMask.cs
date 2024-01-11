#if UNITY_2017_1_OR_NEWER

using UnityEngine;

namespace UnityUtility
{
    public static class LayerMaskExt 
    {
        public static bool Contains(this LayerMask layerMask, int layer)
        {
            return layerMask == (layerMask | (1 << layer));
        }

        public static bool Contains(this LayerMask layerMask, GameObject gameObject)
        {
            return layerMask == (layerMask | (1 << gameObject.layer));
        }
    }
}

#endif
