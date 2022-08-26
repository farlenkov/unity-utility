using System;
using System.Globalization;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityUtility
{
    public class TimeFieldAttribute : PropertyAttribute
    {

    }

#if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(TimeFieldAttribute))]
    public class TimeFieldDrawer : PropertyDrawer
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
            var time_format = @"hh\:mm";
            var time_double = property.doubleValue;
            var time_in = TimeSpan.FromMinutes(time_double);

            var time_str = time_in.ToString(time_format);
            time_str = EditorGUI.TextField(position, label, time_str);

            var is_valid = TimeSpan.TryParseExact(
                time_str,
                time_format,
                CultureInfo.InvariantCulture,
                TimeSpanStyles.None,
                out var time_out);

            if (is_valid)
                property.doubleValue = time_out.TotalMinutes;
        }
    }

#endif
}