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

        // BUTTON

        public static bool Button(string name, float width = 0)
        {
            if (width == 0)
                return GUILayout.Button(name);
            else
                return GUILayout.Button(name, GUILayout.Width(width));
        }

        public static bool PingButton(UnityEngine.Object obj, float width = 0)
        {
            GUI.skin.button.alignment = TextAnchor.MiddleLeft;

            if (width == 0
                ? GUILayout.Button(obj.name) 
                : GUILayout.Button(obj.name, GUILayout.Width(width)))
            {
                EditorGUIUtility.PingObject(obj);
                return true;
            }

            return false;
        }
    }
}
