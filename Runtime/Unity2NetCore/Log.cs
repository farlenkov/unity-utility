
using System;

namespace UnityUtility
{
#if !UNITY_2017_1_OR_NEWER

    public static class Log
    {
        public static void Info(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }

        public static void Error(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }

        public static void InfoEditor(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }

        public static void ErrorEditor(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }

        public static void Exception(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

#endif
}
