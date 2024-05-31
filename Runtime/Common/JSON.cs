using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;

namespace UnityUtility
{
    public static class JSON
    {
        public static T Read<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string Write<T>(T obj, bool formatting = false)
        {
            return JsonConvert.SerializeObject(obj, formatting ? Formatting.Indented : Formatting.None);
        }
    }
}
