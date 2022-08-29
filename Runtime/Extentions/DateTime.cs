using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityUtility
{
    public static class DateTimeExt
    {
        public static long ToUnixMilliseconds(this DateTime date)
        {
            return (long)(date - DateTime.UnixEpoch).TotalMilliseconds;
        }
    }
}