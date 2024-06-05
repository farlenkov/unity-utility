using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtility
{
    public static class IntExt 
    {
        public static string ToStringWithSpaces(this int value)
        {
            return value.ToString("#,#").Replace(',', ' ');
        }

        public static string ToStringWithSign(this int value)
        {
            if (value > 0)
                return $"+{value}";
            else 
                return value.ToString();
        }
    }
}
