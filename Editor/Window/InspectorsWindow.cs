using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;
using Object = UnityEngine.Object;

namespace UnityUtility
{
    public class InspectorsWindow : BaseWindow
    {
        ObjectNameComparer comparer;
        GUIStyle nameStyle;
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
            if (nameStyle == null)
            {
                nameStyle = new GUIStyle(EditorStyles.centeredGreyMiniLabel);
                nameStyle.fixedHeight = 24;
                nameStyle.fontSize = 12;
                nameStyle.fontStyle = FontStyle.Bold;
                nameStyle.normal.textColor = Color.yellow;
            }

            // HEADER

            ScrollNoBars(new Vector2(scroll.x, 0), () => 
            {
                Horizontal()(()=>
                {
                    foreach (var item in Selected)
                        GUILayout.Label(item.name, EditorStyles.whiteLabel, GUILayout.Width(350));
                });
            });

            // INSPECTORS

            scroll = Scroll(scroll, () => 
            {
                Horizontal()(() =>
                {
                    for (var i = 0; i < Selected.Length; i++)
                    {
                        var item = Selected[i];

                        Profiler.BeginSample("InspectorsWindow.Draw(): Item: " + item.name);

                        Vertical(350, () =>
                        {
                            Editor.CreateEditor(item).OnInspectorGUI(); // TODO: cache
                            GUILayout.FlexibleSpace();
                        });

                        Profiler.EndSample();
                    }
                });
            });
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