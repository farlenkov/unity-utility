using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;

#if !UNITY_2017_1_OR_NEWER
using System.Net.Http.Json;
#endif

namespace UnityUtility
{
    public class WebRequest
    {

#if UNITY_2017_1_OR_NEWER

        public static async UniTask<string> Get(string url)
        {
            using (var req = UnityEngine.Networking.UnityWebRequest.Get(url))
            {
                await req.SendWebRequest();
                return req.downloadHandler.text;
            }
        }

        public static async UniTask<string> Post<REQ>(string url, REQ request, string contentType = "application/json")
        {
            var requestString = JsonConvert.SerializeObject(request);

            using (var req = UnityEngine.Networking.UnityWebRequest.Post(url, requestString, contentType))
            {
                await req.SendWebRequest();
                return req.downloadHandler.text;
            }
        }
#else

        public static async UniTask<string> Get(string url)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                var resp = await client.GetAsync(url);

                if (resp.IsSuccessStatusCode)
                {
                    var data = await resp.Content.ReadAsStringAsync();
                    return data;
                }
                else
                {
                    Log.Error($"[WebRequest: Get] {(int)resp.StatusCode}: {resp.ReasonPhrase}");
                    return null;
                }
            }
        }

        public static async UniTask<string> Post<REQ>(string url, REQ request, string contentType = "application/json")
        {
            var requestString = JsonConvert.SerializeObject(request);

            using (var client = new System.Net.Http.HttpClient())
            {
                var resp = await client.PostAsJsonAsync(url, requestString);

                if (resp.IsSuccessStatusCode)
                {
                    var data = await resp.Content.ReadAsStringAsync();
                    return data;
                }
                else
                {
                    Log.Error($"[WebRequest: Post] {(int)resp.StatusCode}: {resp.ReasonPhrase}");
                    return null;
                }
            }
        }

#endif

        public static async UniTask<RESP> Get<RESP>(string url)
        {
            var json = await Get(url);
            return JsonConvert.DeserializeObject<RESP>(json);
        }

        public static async UniTask<RESP> Post<REQ, RESP>(string url, REQ request)
        {
            var json = await Post(url, request);
            return JsonConvert.DeserializeObject<RESP>(json);
        }
    }
}
