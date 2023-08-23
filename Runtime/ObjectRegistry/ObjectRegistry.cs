using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityObjectRegistry
{
    public class ObjectRegistry
    {
        Dictionary<Type, ObjectList> Objects = new();
        List<Type> NewTypes = new();

        // GET

        public bool TryGetAll<TYPE>(out IReadOnlyList<TYPE> result) //where TYPE : MonoBehaviour
        {
            if (!Objects.TryGetValue(typeof(TYPE), out var objects))
            {
                result = default;
                return false;
            }

            result = (objects as ObjectList<TYPE>).All;
            return true;
        }

        public bool TryGetNew<TYPE>(out IReadOnlyList<TYPE> result) //where TYPE : MonoBehaviour
        {
            if (Objects.TryGetValue(typeof(TYPE), out var objects))
            {
                result = default;
                return false;
            }

            result = (objects as ObjectList<TYPE>).New;
            return true;
        }

        // ADD

        public void Add<TYPE>(TYPE theObject) where TYPE : MonoBehaviour
        {
            Add(theObject, theObject.GetType());
        }

        public void Add<TYPE>(TYPE theObject, Type type) //where TYPE : MonoBehaviour
        {
            if (!Objects.TryGetValue(type, out var objects))
            {
                objects = new ObjectList<TYPE>();
                Objects.Add(type, objects);
            }

            (objects as ObjectList<TYPE>).Add(theObject);

            if (!NewTypes.Contains(type))
                NewTypes.Add(type);
        }

        // REMOVE

        public void Remove<TYPE>(TYPE theObject) //where TYPE : MonoBehaviour
        {
            Remove(theObject, theObject.GetType());
        }

        public void Remove<TYPE>(TYPE theObject, Type type) //where TYPE : MonoBehaviour
        {
            if (Objects.TryGetValue(type, out var objects))
                (objects as ObjectList<TYPE>).Remove(theObject);
        }

        public void ClearNew()
        {
            var newCount = NewTypes.Count;

            if (newCount == 0)
                return;

            for (var i = 0; i < newCount; i++)
            {
                var newType = NewTypes[i];

                if (Objects.TryGetValue(newType, out var objects))
                    objects.ClearNew();
            }

            NewTypes.Clear();
        }
    }
}