using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Net.Http;

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

        public static async UniTask<string> PostAndRead<REQ>(
            string url, 
            REQ request, 
            string contentType = "application/json")
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

        public static async UniTask<HttpResponseMessage> Post<REQ>(
            string url, 
            REQ bodyData, 
            string contentType = "application/json")
        {
            var body = bodyData is string bodyStr
                ? bodyStr
                : JsonConvert.SerializeObject(bodyData);

            using (var client = new System.Net.Http.HttpClient())
            {
                var httpContent = new System.Net.Http.StringContent(
                    body,
                    System.Text.Encoding.UTF8,
                    contentType);

                var resp = await client.PostAsync(url, httpContent);
                return resp;
            }
        }

        public static async UniTask<string> PostAndRead<REQ>(
            string url, 
            REQ bodyData, 
            string contentType = "application/json")
        {
            var resp = await Post(url, bodyData, contentType);

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

#endif

        public static async UniTask<RESP> Get<RESP>(string url, bool tryCatch = false)
        {
            var json = await Get(url);

            if (!tryCatch)
                return JsonConvert.DeserializeObject<RESP>(json);

            try
            {
                return JsonConvert.DeserializeObject<RESP>(json);
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                Log.Error("[WebRequest: Get] {0}\nResponse: {1}", url, json);
                return default;
            }
        }

        public static async UniTask<RESP> Post<REQ, RESP>(string url, REQ request)
        {
            var json = await PostAndRead(url, request);
            return JsonConvert.DeserializeObject<RESP>(json);
        }
    }
}
