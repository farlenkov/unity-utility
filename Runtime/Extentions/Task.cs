using System;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using CoreUtils;

namespace UnityUtility
{
    public static class TaskExt
    {
        public static async UniTask<Exception> Catch(
            this UniTask task,
            bool writeLog = false)
        {
            try
            {
                await task;
            }
            catch (OperationCanceledException)
            {
                // nothing
            }
            catch (Exception ex)
            {
                if (writeLog)
                    Log.Exception(ex);

                return ex;
            }

            return default;
        }

        public static async UniTask<(T, Exception)> Catch<T>(
            this UniTask<T> task,
            bool writeLog = false)
        {
            try
            {
                return (await task, default);
            }
            catch (OperationCanceledException)
            {
                // nothing
            }
            catch (Exception ex)
            {
                if (writeLog)
                    Log.Exception(ex);

                return (default, ex);
            }

            return (default, default);
        }

#if UNITY_2017_1_OR_NEWER

        // Not sure is this better than WaitUntil()
        // but looks nicer on caller side.

        public static IEnumerator AsCoroutine(this Task task)
        {
            if (task != null)
                while (!task.IsCompleted)
                    yield return null;
        }

        public static IEnumerator AsCoroutine<T>(this Task<T> task)
        {
            if (task != null)
                while (!task.IsCompleted)
                    yield return null;
        }

        public static IEnumerator AsCoroutine<T>(this UniTask<T>.Awaiter awaiter)
        {
            while (!awaiter.IsCompleted)
                yield return null;
        }

        public static IEnumerator AsCoroutine(this UniTask.Awaiter awaiter)
        {
            while (!awaiter.IsCompleted)
                yield return null;
        }
#endif
    }
}
