using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;

namespace UnityUtility
{
    public class WebRequest
    {
        public static JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

#if UNITY_2017_1_OR_NEWER

        public static async UniTask<string> Get(
            string url, 
            CancellationToken cancellationToken)
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
            CancellationToken cancellationToken,
            string contentType = "application/json")
        {
            var requestString = JsonConvert.SerializeObject(request, SerializerSettings);

            using (var req = UnityEngine.Networking.UnityWebRequest.Post(url, requestString, contentType))
            {
                await req.SendWebRequest();
                return req.downloadHandler.text;
            }
        }
#else

        public static async UniTask<string> Get(
            string url, 
            CancellationToken cancellationToken)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                var resp = await client.GetAsync(url, cancellationToken);

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
            CancellationToken cancellationToken,
            string contentType = "application/json")
        {
            var body = bodyData is string bodyStr
                ? bodyStr
                : JsonConvert.SerializeObject(bodyData, SerializerSettings);

            using (var client = new System.Net.Http.HttpClient())
            {
                var httpContent = new System.Net.Http.StringContent(
                    body,
                    System.Text.Encoding.UTF8,
                    contentType);

                var resp = await client.PostAsync(url, httpContent, cancellationToken);
                return resp;
            }
        }

        public static async UniTask<string> PostAndRead<REQ>(
            string url, 
            REQ bodyData, 
            CancellationToken cancellationToken,
            string contentType = "application/json")
        {
            var resp = await Post(url, bodyData, cancellationToken, contentType);

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

        public static async UniTask<RESP> Get<RESP>(
            string url, 
            CancellationToken cancellationToken,
            bool tryCatch = false)
        {
            var json = await Get(url, cancellationToken);

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

        public static async UniTask<RESP> Post<REQ, RESP>(
            string url, 
            REQ request,
            CancellationToken cancellationToken)
        {
            var json = await PostAndRead(url, request, cancellationToken);
            return JsonConvert.DeserializeObject<RESP>(json);
        }
    }
}
