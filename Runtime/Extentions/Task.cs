using System;
using System.Threading.Tasks;

namespace UnityUtility
{
    public static class TaskExt
    {
        public static async Task<Exception> Catch(
            this Task task,
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

        public static async Task<(T, Exception)> Catch<T>(
            this Task<T> task, 
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
    }
}
