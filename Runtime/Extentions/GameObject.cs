#if UNITY_2017_1_OR_NEWER

using System.Collections.Generic;
using UnityEngine;

namespace UnityUtility
{
    public static class GameObjectExt
    {
        public static void SetActive(this IList<GameObject> gameObjects, bool value)
        {
            var count = gameObjects.Count;

            for (var i = 0; i < count; i++)
            {
                var gameObject = gameObjects[i];

                if (gameObject != null)
                    gameObject.SetActive(value);
            }
        }
        
        public static void SetActive(this IList<Component> gameObjects, bool value)
        {
            var count = gameObjects.Count;

            for (var i = 0; i < count; i++)
            {
                var gameObject = gameObjects[i];

                if (gameObject != null)
                    gameObject.gameObject.SetActive(value);
            }
        }

        public static void Destroy(this IList<GameObject> gameObjects, bool clear = true)
        {
            var count = gameObjects.Count;

            for (var i = 0; i < count; i++)
            {
                var gameObject = gameObjects[i];

                if (gameObject != null)
                    UnityEngine.Object.Destroy(gameObject);
            }

            if (clear)
                gameObjects.Clear();
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