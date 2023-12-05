using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnityUtility
{
    public class WebRequest
    {
        public static async UniTask<string> Get(string url)
        {
            using (var req = UnityEngine.Networking.UnityWebRequest.Get(url))
            {
                await req.SendWebRequest();
                return req.downloadHandler.text;
            }
        }

        public static async UniTask<RESP> Get<RESP>(string url)
        {
            var json = await Get(url);
            return JsonUtility.FromJson<RESP>(json);
        }

        public static async UniTask<string> Post<REQ>(string url, REQ request)
        {
            var requestString = JsonUtility.ToJson(request);

            using (var req = UnityEngine.Networking.UnityWebRequest.Post(url, requestString, "application/json"))
            {
                await req.SendWebRequest();
                return req.downloadHandler.text;
            }
        }

        public static async UniTask<RESP> Post<REQ, RESP>(string url, REQ request)
        {
            var json = await Post(url, request);
            return JsonUtility.FromJson<RESP>(json);
        }
    }
}