using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityUtility
{
    public abstract class BaseEditor : Editor
    {
        // CHANGE CHECK

        public static bool ChangeCheck(Action callback)
        {
            EditorGUI.BeginChangeCheck();
            callback();
            return EditorGUI.EndChangeCheck();
        }

        // DIRTY

        public static void SetDirty(UnityEngine.Object target)
        {
            EditorUtility.SetDirty(target);
        }
        
        public static bool IsDirty(UnityEngine.Object target)
        {
            return EditorUtility.IsDirty(target);
        }
    }
}
