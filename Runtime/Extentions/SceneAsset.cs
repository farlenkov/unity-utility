#if UNITY_EDITOR

using System.IO;
using UnityEditor;

namespace UnityUtility
{
    public static class SceneAssetExt
    {
        public static string GetSceneName(this SceneAsset sceneAsset)
        {
            if (sceneAsset == null)
                return null;

            var scenePath = AssetDatabase.GetAssetPath(sceneAsset);
            var sceneName = Path.GetFileNameWithoutExtension(scenePath);
            return sceneName;
        }
    }
}

#endif