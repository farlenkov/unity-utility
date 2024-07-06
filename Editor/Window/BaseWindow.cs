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

        // SCROLL

        Queue<Vector2> ScrollQueue = new ();

        protected void Scroll(Action callback)
        {
            var scroll = ScrollQueue.Count == 0
                ? Vector2.zero
                : ScrollQueue.Dequeue();

            scroll = EditorGUILayout.BeginScrollView(scroll);

            callback();

            ScrollQueue.Enqueue(scroll);
            EditorGUILayout.EndScrollView();
        }

        protected void VerticalScroll(Action callback)
        {
            Scroll(() => 
            {
                Vertical(() =>
                {
                    callback();
                });
            });
        }

        // BUTTON

        public static bool Button(string name, float width = 0)
        {
            if (width == 0)
                return GUILayout.Button(name);
            else
                return GUILayout.Button(name, GUILayout.Width(width));
        }

        // STYLE

        public static void ButtonAlignment(TextAnchor alignment)
        {
            GUI.skin.button.alignment = alignment;
        }

        // CHANGE CHECK

        public static bool ChangeCheck(Action callback1)
        {
            EditorGUI.BeginChangeCheck();
            callback1();
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
