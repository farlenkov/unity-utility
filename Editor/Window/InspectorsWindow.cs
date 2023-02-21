using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;
using Object = UnityEngine.Object;

namespace UnityUtility
{
    public class InspectorsWindow : EditorWindow
    {
        ObjectNameComparer comparer;
        GUIStyle name_style;
        Object[] Selected;
        Vector2 scroll;

        [MenuItem("Window/Inspectors")]
        static void Init()
        {
            GetWindow<InspectorsWindow>("Inspectors");
        }

        void Update()
        {
            if (comparer == null)
                comparer = new ObjectNameComparer();

            var selected = Selection.GetFiltered<Object>(SelectionMode.Assets);
            Array.Sort(selected, comparer);

            if (Selected == null && selected.Length > 0 ||
                Selected != null && Selected.Length != selected.Length ||
                Selected != null && Selected.Length == 1 && selected.Length == 1 && Selected[0].name != selected[0].name)
            {
                Selected = selected;
                Repaint();
            }
        }

        void OnGUI()
        {
            if (Selected == null ||
                Selected.Length == 0)
            {
                GUILayout.Label("Select Assets...");
            }
            else
            {
                DrawInspectors();
            }
        }

        void DrawInspectors()
        {
            if (name_style == null)
            {
                name_style = new GUIStyle(EditorStyles.centeredGreyMiniLabel);
                name_style.fixedHeight = 24;
                name_style.fontSize = 12;
                name_style.fontStyle = FontStyle.Bold;
                name_style.normal.textColor = Color.yellow;
            }

            // HEADER

            EditorGUILayout.BeginScrollView(new Vector2(scroll.x, 0), false, false, new GUIStyle(), new GUIStyle(), new GUIStyle());

            GUILayout.BeginHorizontal();

            foreach (var item in Selected)
                GUILayout.Label(item.name, EditorStyles.whiteLabel, GUILayout.Width(350));

            GUILayout.EndHorizontal();
            EditorGUILayout.EndScrollView();

            // INSPECTORS

            scroll = EditorGUILayout.BeginScrollView(scroll);
            GUILayout.BeginHorizontal();

            for (var i = 0; i < Selected.Length; i++)
            {
                var item = Selected[i];

                Profiler.BeginSample("InspectorsWindow.Draw(): Item: " + item.name);
                GUILayout.BeginVertical(GUILayout.Width(350));

                Editor.CreateEditor(item).OnInspectorGUI(); // TODO: cache

                GUILayout.FlexibleSpace();
                GUILayout.EndVertical();
                Profiler.EndSample();
            }

            GUILayout.EndHorizontal();
            EditorGUILayout.EndScrollView();
        }

        class ObjectNameComparer : IComparer<Object>
        {
            public int Compare(Object x, Object y)
            {
                return x.name.CompareTo(y.name);
            }
        }
    }
}