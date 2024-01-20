#if UNITY_EDITOR

using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;

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

        public static void OpenSingle(this SceneAsset sceneAsset)
        {
            var scenePath = AssetDatabase.GetAssetPath(sceneAsset);
            EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
        }

        public static void OpenAdditive(this SceneAsset sceneAsset)
        {
            var scenePath = AssetDatabase.GetAssetPath(sceneAsset);
            EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
        }
    }
}

#endif