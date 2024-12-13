using CoreUtils;
using Newtonsoft.Json.Linq;

namespace UnityUtility
{
    public static class JTokenExt
    {
        public static T Parse<T>(this JToken token, string key)
        {
            if (token == null)
                return default;

            var value = token[key];

            if (value == null)
                return default;
            else
                return JSON.parse<T>(value.ToString());                
        }
    }
}