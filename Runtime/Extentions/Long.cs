using System;

namespace UnityUtility
{
    public static class LongExt
    {
        public static DateTime ToDate (this long unixTimeMilliseconds)
        {
            return DateTime.UnixEpoch.AddMilliseconds(unixTimeMilliseconds);
        }
    }
}
