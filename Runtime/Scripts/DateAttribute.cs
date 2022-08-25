using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

namespace UnityUtility
{
    public class DateAttribute : PropertyAttribute
    {

    }

#if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(DateAttribute))]
    public class DateDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(
            SerializedProperty property,
            GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(
            Rect position,
            SerializedProperty property,
            GUIContent label)
        {
            if (property.hasMultipleDifferentValues)
            {
                var color = GUI.contentColor;
                GUI.contentColor = new Color(1f, 1f, 1f, 0.5f);
                EditorGUI.TextField(position, label, EditorGUIUtility.TrTextContent("—", "Mixed Values").text);
                //EditorGUI.PropertyField(position, property, label, true);

                //GUI.changed = false;
                //var value = (int)(EnemyType)EditorGUI.EnumPopup(position, (EnemyType)_type.intValue);
                //if (GUI.changed)
                //    _type.intValue = value;

                GUI.contentColor = color;
                return;
            }
            
            var date_format = "dd-MM-yyyy";
            var date_long = property.longValue;
            var date_in = DateTimeOffset.FromUnixTimeMilliseconds(date_long);

            var date_str = date_in.ToString(date_format);
            date_str = EditorGUI.TextField(position, label, date_str);

            var is_valid = DateTimeOffset.TryParseExact(
                date_str,
                date_format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal,
                out var date_out);

            if (is_valid)
                property.longValue = date_out.ToUnixTimeMilliseconds();
        }
    }

#endif
}