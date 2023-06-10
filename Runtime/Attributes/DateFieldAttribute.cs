using System;
using System.Globalization;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityUtility
{
    public class DateFieldAttribute : PropertyAttribute
    {

    }

#if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(DateFieldAttribute))]
    public class DateFieldDrawer : PropertyDrawer
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
                EditorGUI.TextField(position, label, EditorGUIUtility.TrTextContent("�", "Mixed Values").text);
                //EditorGUI.PropertyField(position, property, label, true);

                //GUI.changed = false;
                //var value = (int)(EnemyType)EditorGUI.EnumPopup(position, (EnemyType)_type.intValue);
                //if (GUI.changed)
                //    _type.intValue = value;

                GUI.contentColor = color;
                return;
            }

            var dateFormat = "dd-MM-yyyy";
            var dateLong = property.longValue;
            var dateIn = dateLong.ToDate();

            var dateStr = dateIn.ToString(dateFormat);
            dateStr = EditorGUI.TextField(position, label, dateStr);

            var isValid = DateTime.TryParseExact(
                dateStr,
                dateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AdjustToUniversal,
                out var date_out);

            if (isValid)
                property.longValue = date_out.ToUnixMilliseconds();
        }
    }

#endif
}