
#if !UNITY_2017_1_OR_NEWER

using System;

namespace UnityEngine
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public abstract class PropertyAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class HideInInspectorAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class SerializeFieldAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class SpaceAttribute : Attribute { public SpaceAttribute() { } public SpaceAttribute(int size) { } }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class HeaderAttribute : Attribute { public HeaderAttribute(string text) { } }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class TextAreaAttribute : Attribute
    {
        public TextAreaAttribute() { }
        public TextAreaAttribute(int val1, int val2) { }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ContextMenuAttribute : Attribute { public ContextMenuAttribute(string text) { } }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class Tooltip : Attribute { public Tooltip(string text) { } }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class RangeAttribute : Attribute
    {
        public RangeAttribute(int val1, int val2) { }
        public RangeAttribute(float val1, float val2) { }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class MinAttribute : Attribute
    {
        public MinAttribute(int val1) { }
        public MinAttribute(float val1) { }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CreateAssetMenu : Attribute
    {
        public string fileName;
        public string menuName;
    }
}

namespace UnityEngine.Scripting
{
    public class PreserveAttribute : Attribute { }
}

#endif
