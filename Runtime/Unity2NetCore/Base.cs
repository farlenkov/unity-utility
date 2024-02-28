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

    public struct Vector2
    {
        public float x;
        public float y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }

#endif
}