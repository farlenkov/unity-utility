using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtility
{
    public abstract class ObjectWithRegistry<T> : MonoBehaviour
        where T : ObjectWithRegistry<T>
    {
        protected abstract List<T> Registry { get; }

        void OnEnable()
        {
            Registry.Add((T)this);
        }

        void OnDisable()
        {
            var index = Registry.IndexOf((T)this);

            if (index >= 0)
                Registry.RemoveAt(index);
        }
    }
}
