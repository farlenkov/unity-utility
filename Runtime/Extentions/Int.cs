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
    }
}
