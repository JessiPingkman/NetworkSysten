using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using _Scripts.Utils.Extensions;
using _Scripts.Utils.Network.ResultHandles;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace _Scripts.Utils.Network
{
    public struct HeaderOption
    {
        public (string Name, string Value) Option;
    }

    public static class Network
    {
        private const string MAIN_URL = "https://envisiondev.xyz/api";

        public static async Task<Result> Get<T>(string api, List<HeaderOption> headerOptions) where T : BaseModel
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                return new ErrorResult(new ConnectionError());
            }

            try
            {
                UnityWebRequest response = UnityWebRequest.Get($"{MAIN_URL}/{api}");
                if (headerOptions.Count > 2)
                {
                    throw new AggregateException();
                }

                for (int i = 0; i < headerOptions.Count; i++)
                {
                    response.SetRequestHeader(headerOptions[i].Option.Name, headerOptions[i].Option.Value);
                }

                var result = await response.SendWebRequest();
                if (result is UnityWebRequest.Result.DataProcessingError)
                {
                    return new ErrorResult(new ServiceError());
                }

                if (result is UnityWebRequest.Result.ProtocolError)
                {
                    return new ErrorResult(new ResponseError());
                }

                return new SuccessfulResult<T>(JsonConvert.DeserializeObject<T>(response.downloadHandler.text));
            }
            catch (HttpRequestException httpRequestException)
            {
                throw httpRequestException;
            }
        }
    }
}