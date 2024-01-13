#if UNITY_2017_1_OR_NEWER

using System.Collections.Generic;
using UnityEngine;

namespace UnityUtility
{
    public static class GameObjectExt
    {
        public static void SetActive(this GameObject[] gameObjects, bool value)
        {
            var count = gameObjects.Length;

            for (var i = 0; i < count; i++)
            {
                var gameObject = gameObjects[i];

                if (gameObject != null)
                    gameObject.SetActive(value);
            }
        }

        public static void SetActive(this List<GameObject> gameObjects, bool value)
        {
            var count = gameObjects.Count;

            for (var i = 0; i < count; i++)
            {
                var gameObject = gameObjects[i];

                if (gameObject != null)
                    gameObject.SetActive(value);
            }
        }

        public static bool InLayers(this GameObject gameObject, LayerMask layerMask)
        {
            return layerMask == (layerMask | (1 << gameObject.layer));
        }

        public static string GetRootName(this GameObject gameObject)
        {
            if (!string.IsNullOrEmpty(gameObject.scene.name))
                return gameObject.scene.name;

            if (gameObject.transform.parent != null)
                return gameObject.transform.parent.gameObject.GetRootName();

            return gameObject.name;
        }
    }
}

#endif