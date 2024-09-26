using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;

namespace UnityUtility
{
    public static class JSON
    {
        public static T parse<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string stringify<T>(T obj, bool formatting = false)
        {
            return JsonConvert.SerializeObject(obj, formatting 
                ? FormattingIndented 
                : FormattingNone);
        }

        public static JsonSerializerSettings FormattingNone = new ()
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.None
        };

        public static JsonSerializerSettings FormattingIndented = new ()
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented
        };
    }
}
