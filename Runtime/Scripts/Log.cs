using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtility
{

#if UNITY_2017_1_OR_NEWER

    public static class Log
    {
        public static void Info(string message, params object[] args)
        {
            if (Application.isEditor)
                Debug.LogFormat(message, args);
            else
                Debug.LogFormat($"{DateTime.UtcNow} {message}", args);
        }

        public static void Error(string message, params object[] args)
        {
            if (Application.isEditor)
                Debug.LogErrorFormat(message, args);
            else
                Debug.LogErrorFormat($"{DateTime.UtcNow} {message}", args);
        }

        public static void Exception(Exception ex)
        {
            Debug.LogException(ex);
        }

        // EDITOR ONLY      

        public static void InfoEditor(string message, params object[] args)
        {
            if (Application.isEditor)
                Debug.LogFormat(message, args);
        }

        public static void ErrorEditor(string message, params object[] args)
        {
            if (Application.isEditor)
                Debug.LogErrorFormat(message, args);
        }

        public static void ExceptionEditor(Exception ex)
        {
            if (Application.isEditor)
                Debug.LogException(ex);
        }
    }

#endif

}