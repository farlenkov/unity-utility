using System.Net.Http;
using System.Threading.Tasks;
using CoreUtils;

namespace UnityUtility
{
    public static class HttpResponseMessageExt
    {
        public static async Task<T> ReadAsJson<T>(this HttpResponseMessage resp)
        {
            var content = await resp.Content.ReadAsStringAsync();
            return JSON.parse<T>(content);
        }
    }
}