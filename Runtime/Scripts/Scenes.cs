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

        public static void LoadNext()
        {
            if (OnChangeSceneStart != null)
                OnChangeSceneStart();

            SceneManager.LoadScene(CurrentIndex + 1);

            if (OnChangeSceneComplete != null)
                OnChangeSceneComplete();
        }

        // LOAD by NAME

        public static void Load(string scene_name)
        {
            if (OnChangeSceneStart != null)
                OnChangeSceneStart();

            SceneManager.LoadScene(scene_name);

            if (OnChangeSceneComplete != null)
                OnChangeSceneComplete();
        }

        public static AsyncOperation LoadAdditiveAsync(string scene_name)
        {
            return SceneManager.LoadSceneAsync(scene_name, LoadSceneMode.Additive);
        }
    
        // UNLOAD

        public static AsyncOperation UnloadCurrent()
        {
            return SceneManager.UnloadSceneAsync(CurrentIndex);
        }
    }

#endif

}