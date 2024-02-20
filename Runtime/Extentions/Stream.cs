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
    }
}
