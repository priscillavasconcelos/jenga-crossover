using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Jenga.APICommunication
{
    public class APIComManager
    {
        private readonly string url = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com";
        private readonly string path = "Assessment/stack";
        private int timeout = 10;
        
        public async void GetAPIInfo<T>(Action<List<T>> action)
        {
            using UnityWebRequest request = new UnityWebRequest();

            request.url = $"{url}/{path}";
            request.timeout = timeout;

            request.method = "GET";
            request.downloadHandler = new DownloadHandlerBuffer();

            var operation = request.SendWebRequest();

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            switch (request.result)
            {
                case UnityWebRequest.Result.Success:
                    string response = request.downloadHandler.text;
                    if (response != null)
                    {
                        var formatedResponse = JsonConvert.DeserializeObject<List<T>>(response);
                        action(formatedResponse);
                    }
                    break;
                default:
                    Debug.LogErrorFormat("Error on getting info: {0}", request.result);
                    action(new List<T>());
                    break;
            }
        }
    }
}