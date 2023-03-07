using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityServiceLocator
{
    public static class ServiceLocator
    {
        static Dictionary<Type, object> Services = new Dictionary<Type, object>();
        static Dictionary<Type, IServiceFactory> FactoriesByServiceType = new Dictionary<Type, IServiceFactory>();

        // SERVICES

        public static bool TryGetService<T>(out T result)
        {
            if (Services.TryGetValue(typeof(T), out var service))
            {
                if (service != default)
                {
                    result = (T)service;
                    return true;
                }
            }

            result = default;
            return false;
        }

        public static bool TryAddService<T>(T service)
        {
            var serviceType = typeof(T);

            if (Services.ContainsKey(serviceType))
                return false;
            
            Services.Add(serviceType, service);
            return true;
        }

        public static bool TryRemoveService<T>(T service_to_remove)
        {
            var serviceType = typeof(T);

            if (Services.TryGetValue(serviceType, out var service))
            {
                if (service_to_remove.Equals(service))
                {
                    Services.Remove(serviceType);
                    return true;
                }
            }

            return false;
        }

        // FACTORIES

        public static bool TryAddFactory<T>(IServiceFactory factory)
        {
            var serviceType = typeof(T);

            if (FactoriesByServiceType.ContainsKey(serviceType))
                return false;

            FactoriesByServiceType.Add(serviceType, factory);
            return true;
        }

        public static bool TryRemoveFactory<T>(IServiceFactory factory_to_remove)
        {
            var serviceType = typeof(T);

            if (FactoriesByServiceType.TryGetValue(serviceType, out var factory))
            {
                if (factory_to_remove.Equals(factory))
                {
                    FactoriesByServiceType.Remove(serviceType);
                    return true;
                }
            }

            return false;
        }
    }
}