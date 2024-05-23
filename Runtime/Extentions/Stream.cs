using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace UnityUtility
{
    public static class StreamExt
    {
        public static async Task<string> ReadAsStringAsync(this Stream requestBody)
        {
            using (var reader = new StreamReader(requestBody))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public static async Task<T> ReadAsJsonAsync<T>(this Stream requestBody)
        {
            var json = await requestBody.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
