using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtility
{
    public class ScriptableObjectComparer : IComparer<ScriptableObject>
    {
        public int Compare(ScriptableObject x, ScriptableObject y)
        {
            return CompareObjects(x, y);
        }

        public static int CompareObjects(ScriptableObject x, ScriptableObject y)
        {
            if (x == null && 
                y == null)
                return 0;

            if (x == null)
                return 1;

            if (y == null)
                return -1;

            return x.name.CompareTo(y.name);
        }
    }    
}
