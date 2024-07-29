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

        protected Vector2 Scroll(Vector2 scroll, Action callback)
        {
            scroll = EditorGUILayout.BeginScrollView(scroll);
            callback();
            EditorGUILayout.EndScrollView();
            return scroll;
        }

        protected Vector2 ScrollNoBars(Vector2 scroll, Action callback)
        {
            scroll = EditorGUILayout.BeginScrollView(scroll, false, false, GUIStyle.none, GUIStyle.none, GUIStyle.none);
            callback();
            EditorGUILayout.EndScrollView();
            return scroll;
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

        // STYLE

        public static void ButtonAlignment(TextAnchor alignment)
        {
            GUI.skin.button.alignment = alignment;
        }

        // CHANGE CHECK

        public static bool ChangeCheck(Action callback)
            => BaseEditor.ChangeCheck(callback);

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
