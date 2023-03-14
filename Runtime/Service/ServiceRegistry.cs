using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtility;

namespace UnityServiceRegistry
{
    public static class ServiceRegistry
    {
        static Dictionary<Type, object> Services = new Dictionary<Type, object>();
        static Dictionary<Type, List<object>> ServiceAddListners = new Dictionary<Type, List<object>>();
        static Dictionary<Type, List<object>> ServiceRemoveListners = new Dictionary<Type, List<object>>();

        // SERVICES - GET

        public static void GetService<INTERFACE>(out INTERFACE result)
        {
            if (TryGetService<INTERFACE>(out var service))
                result = service;
            else
                throw new Exception($"Service '{typeof(INTERFACE).Name}' not found");
        }

        public static INTERFACE GetService<INTERFACE>()
        {
            if (TryGetService<INTERFACE>(out var service))
                return service;
            else
                throw new Exception($"Service '{typeof(INTERFACE).Name}' not found");
        }

        public static bool TryGetService<INTERFACE>(out INTERFACE result)
        {
            if (Services.TryGetValue(typeof(INTERFACE), out var service))
            {
                if (service != default)
                {
                    result = (INTERFACE)service;
                    return true;
                }
            }

            result = default;
            return false;
        }

        // SERVICES - ADD

        public static bool TryAddService<IMPLEMENTATION>(IMPLEMENTATION serviceToAdd)
        {
            if (serviceToAdd == null)
                return false;

            return TryAddService<IMPLEMENTATION,IMPLEMENTATION>(
                serviceToAdd);
        }

        public static bool TryAddService<INTERFACE, IMPLEMENTATION>(IMPLEMENTATION serviceToAdd) where IMPLEMENTATION : INTERFACE
        {
            if (serviceToAdd == null)
                return false;

            var interfaceType = typeof(INTERFACE);
            var implementationType = typeof(IMPLEMENTATION);

            if (Services.ContainsKey(interfaceType))
                return false;

            Services.Add(interfaceType, serviceToAdd);

            if (ServiceAddListners.TryGetValue(interfaceType, out var listners))
            {
                for (var i = 0; i < listners.Count; i++)
                {
                    var callback = listners[i] as Action<INTERFACE>;
                    callback((INTERFACE)serviceToAdd);
                }
            }

            return true;
        }

        // SERVICES - REMOVE

        public static bool TryRemoveService<IMPLEMENTATION>(IMPLEMENTATION serviceToRemove)
        {
            if (serviceToRemove == null) 
                return false;

            return TryRemoveService<IMPLEMENTATION, IMPLEMENTATION>(serviceToRemove);
        }

        public static bool TryRemoveService<INTERFACE, IMPLEMENTATION>(IMPLEMENTATION serviceToRemove) where IMPLEMENTATION : INTERFACE
        {
            if (serviceToRemove == null)
                return false;

            var interfaceType = typeof(INTERFACE);

            if (Services.TryGetValue(interfaceType, out var service))
            {
                if (serviceToRemove.Equals(service))
                {
                    Services.Remove(interfaceType);

                    if (ServiceRemoveListners.TryGetValue(interfaceType, out var listners))
                    {
                        for (var i = 0; i < listners.Count; i++)
                        {
                            var callback = (Action<INTERFACE>)listners[i];
                            callback((INTERFACE)serviceToRemove);
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        // SERVICES - WATCH

        public static void OnServiceAdd<INTERFACE>(Action<INTERFACE> callback)
        {
            if (callback == null)
                return;

            var interfaceType = typeof(INTERFACE);
            var listners = ServiceAddListners.GetItem(interfaceType);

            if (!listners.Contains(callback))
                listners.Add(callback);
        }

        public static void OnServiceRemove<INTERFACE>(Action<INTERFACE> callback)
        {
            if (callback == null)
                return;

            if (callback == null)
                return;

            var interfaceType = typeof(INTERFACE);
            var listners = ServiceRemoveListners.GetItem(interfaceType);

            if (!listners.Contains(callback))
                listners.Add(callback);
        }

        public static void OffServiceAdd<INTERFACE>(Action<INTERFACE> callback)
        {
            if (callback == null)
                return;

            var interfaceType = typeof(INTERFACE);

            if (ServiceAddListners.TryGetValue(interfaceType, out var listners))
                listners.TryRemove(callback);
        }

        public static void OffServiceRemove<INTERFACE>(Action<INTERFACE> callback)
        {
            if (callback == null)
                return;

            var interfaceType = typeof(INTERFACE);

            if (ServiceRemoveListners.TryGetValue(interfaceType, out var listners))
                listners.TryRemove(callback);
        }
    }
}