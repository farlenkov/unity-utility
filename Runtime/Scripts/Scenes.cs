using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityUtility
{
    public static class Scenes
    {
        public static event Action OnChangeSceneStart;
        public static event Action OnChangeSceneComplete;

        public static int CurrentIndex => SceneManager.GetActiveScene().buildIndex;

        public static void LoadNext()
        {
            if (OnChangeSceneStart != null)
                OnChangeSceneStart();

            SceneManager.LoadScene(CurrentIndex + 1);

            if (OnChangeSceneComplete != null)
                OnChangeSceneComplete();
        }
    }
}