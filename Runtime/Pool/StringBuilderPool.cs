using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace UnityUtility
{
    public static class StringBuilderPool
    {
        static ConcurrentQueue<StringBuilder> pool = new();

        public static StringBuilder Get()
        {
            if (!pool.TryDequeue(out var builder))
                builder = new StringBuilder();

            return builder;
        }

        public static string Release(
            this StringBuilder stringBuilder, 
            bool returnContent = true)
        {
            var content = returnContent ? stringBuilder.ToString() : default;
            stringBuilder.Clear();
            pool.Enqueue(stringBuilder);
            return content;
        }
    }
}
