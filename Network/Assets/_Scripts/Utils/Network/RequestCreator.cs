using UnityEngine.Networking;

namespace _Scripts.Utils.Network
{
    public struct RequestCreator
    {
        private const int REQUEST_TIMEOUT_HANDLING_IN_SECONDS = 100;

        public UnityWebRequest CreateRequestWithDefaultHeader(UnityWebRequest request)
        {
            request.SetRequestHeader("ContentType", "application/json");
            request.timeout = REQUEST_TIMEOUT_HANDLING_IN_SECONDS;
            return request;
        }

        public UnityWebRequest CreateRequestWithDefaultHeaderAndBearerToken(UnityWebRequest request, string token)
        {
            request.SetRequestHeader("ContentType", "application/json");
            request.SetRequestHeader("Authorization", $"Bearer {token}");
            request.timeout = REQUEST_TIMEOUT_HANDLING_IN_SECONDS;
            return request;
        }
    }
}