#if UNITY_2017_1_OR_NEWER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityUtility
{
    public static class TransformExt
    {
        public static void ForceRebuildLayout(this Transform tm)
        {
            for (int i = 0; i < tm.childCount; ++i)
                tm.GetChild(i).ForceRebuildLayout();

            if (tm is RectTransform rect)
                LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        }
    }
}

#endif