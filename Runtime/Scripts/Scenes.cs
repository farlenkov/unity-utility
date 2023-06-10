using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_2017_1_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace UnityUtility
{

#if UNITY_2017_1_OR_NEWER

    public static class Scenes
    {
        // EVENTS

        public static event Action OnChangeSceneStart;
        public static event Action OnChangeSceneComplete;

        // PROPS

        public static int CurrentIndex => SceneManager.GetActiveScene().buildIndex;

        // LOAD by INDEX

        public static void LoadNext(bool writeLog = false)
        {
            if (OnChangeSceneStart != null)
                OnChangeSceneStart();

            var nextIndex = CurrentIndex + 1;
            SceneManager.LoadScene(nextIndex);

            if (writeLog)
            {
                for (var i = 0; i < SceneManager.sceneCount; i++)
                    Log.Info("[UnityUtility.Scenes: LoadNext] {0} / {1} / {2}", nextIndex, i, SceneManager.GetSceneAt(i).name);
            }

            if (OnChangeSceneComplete != null)
                OnChangeSceneComplete();
        }

        // LOAD by NAME

        public static void Load(string sceneName)
        {
            if (OnChangeSceneStart != null)
                OnChangeSceneStart();

            SceneManager.LoadScene(sceneName);

            if (OnChangeSceneComplete != null)
                OnChangeSceneComplete();
        }

        public static AsyncOperation LoadAdditiveAsync(string sceneName)
        {
            return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        // UNLOAD

        public static AsyncOperation UnloadCurrent()
        {
            return SceneManager.UnloadSceneAsync(CurrentIndex);
        }

        // SET ACTIVE

        public static void SetActiveScene(string sceneName)
        {
            if (string.IsNullOrEmpty(sceneName))
                return;

            var scene = SceneManager.GetSceneByName(sceneName);

            if (scene != null)
                SceneManager.SetActiveScene(scene);
        }
    }

#endif

}