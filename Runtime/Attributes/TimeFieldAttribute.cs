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
            var timeFormat = @"hh\:mm";
            var timeDouble = property.doubleValue;
            var timeIn = TimeSpan.FromMinutes(timeDouble);

            var timeStr = timeIn.ToString(timeFormat);
            timeStr = EditorGUI.TextField(position, label, timeStr);

            var isValid = TimeSpan.TryParseExact(
                timeStr,
                timeFormat,
                CultureInfo.InvariantCulture,
                TimeSpanStyles.None,
                out var timeOut);

            if (isValid)
                property.doubleValue = timeOut.TotalMinutes;
        }
    }

#endif
}