#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

namespace UnityUtility
{
    public static class ObjectExt
    {
        public static string GetAssetPath(this Object unityObject)
        {
            if (unityObject == null)
                return null;

            var assetPath = AssetDatabase.GetAssetPath(unityObject);
            return assetPath;
        }

        public static string GetAssetGUID(this Object unityObject)
        {
            if (unityObject == null)
                return null;

            var assetPath = AssetDatabase.GetAssetPath(unityObject);
            var assetGuid = AssetDatabase.AssetPathToGUID(assetPath);
            return assetGuid;
        }
    }
}

#endif