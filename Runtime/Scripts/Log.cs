using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtility
{
    public static class Log
    {
        public static void Info(string message, params object[] args)
        {
            Debug.LogFormat(message, args);
        }

        public static void Error(string message, params object[] args)
        {
            Debug.LogErrorFormat(message, args);
        }

        // EDITOR ONLY      

        public static void InfoEditor(string message, params object[] args)
        {
            if (Application.isEditor)
                Debug.LogFormat(message, args);
        }
    }
}