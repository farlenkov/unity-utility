using System.Collections.Concurrent;

namespace UnityUtility
{
    public static class ObjectPool<T> where T : new()
    {
        static ConcurrentQueue<T> pool = new();

        public static T Get()
        {
            if (pool.TryDequeue(out var obj))
                return obj;
            else
                return new T();
        }

        public static void Release(T obj)
        {
            pool.Enqueue(obj);
        }        
    }

    public static class ObjectPoolExt
    {
        public static void Release<T>(this T obj) where T : new()
        {
            ObjectPool<T>.Release(obj);
        }
    }
}