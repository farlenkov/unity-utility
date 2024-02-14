namespace UnityEngine
{
#if !UNITY_2017_1_OR_NEWER

    public class Application
    {
        public static string dataPath => System.AppDomain.CurrentDomain.BaseDirectory;
    }

    public class ScriptableObject
    {
        public string name;
    }

#endif
}