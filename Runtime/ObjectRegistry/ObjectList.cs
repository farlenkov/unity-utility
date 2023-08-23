using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityObjectRegistry
{
    internal abstract class ObjectList
    {
        public abstract void ClearNew();
    }

    internal class ObjectList<TYPE> : ObjectList // where TYPE : MonoBehaviour
    {
        List<TYPE> allObjects = new();
        List<TYPE> newObjects = new();

        public IReadOnlyList<TYPE> All => allObjects;
        public IReadOnlyList<TYPE> New => newObjects;

        public void Add(TYPE theObject)
        {
            allObjects.Add(theObject);
            newObjects.Add(theObject);
        }

        public void Remove(TYPE theObject)
        {
            var allIndex = allObjects.IndexOf(theObject);
            var newIndex = newObjects.IndexOf(theObject);

            if (allIndex >= 0)
                allObjects.RemoveAt(allIndex);

            if (newIndex >= 0)
                newObjects.RemoveAt(newIndex);
        }

        public override void ClearNew()
        {
            newObjects.Clear();
        }
    }
}
