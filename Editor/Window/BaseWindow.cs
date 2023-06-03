using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityUtility
{
    public abstract class BaseWindow : EditorWindow
    {
        public static void Vertical(Action callback)
        {
            GUILayout.BeginVertical();
            callback();
            GUILayout.EndVertical();
        }
        
        public static void Vertical(float width, Action callback)
        {
            GUILayout.BeginVertical(GUILayout.Width(width));
            callback();
            GUILayout.EndVertical();
        }

        public static Action<Action> Vertical(params GUILayoutOption[] options)
        {
            return (callback) =>
            {
                GUILayout.BeginVertical(options);
                callback();
                GUILayout.EndVertical();
            };
        }

        public static void Horizontal(Action callback)
        {
            GUILayout.BeginHorizontal();
            callback();
            GUILayout.EndHorizontal();
        }
        
        public static void Horizontal(float width, Action callback)
        {
            GUILayout.BeginHorizontal(GUILayout.Width(width));
            callback();
            GUILayout.EndHorizontal();
        }

        public static Action<Action> Horizontal(params GUILayoutOption[] options)
        {
            return (callback) =>
            {
                GUILayout.BeginHorizontal(options);
                callback();
                GUILayout.EndHorizontal();
            };
        }
    }
}
