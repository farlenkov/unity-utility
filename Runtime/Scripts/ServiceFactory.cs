using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityServiceLocator
{
    public interface IServiceFactory
    {
        public bool TryGetService<T>(out T result);
    }
}