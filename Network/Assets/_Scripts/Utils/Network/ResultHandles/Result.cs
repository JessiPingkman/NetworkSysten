using Newtonsoft.Json;

namespace _Scripts.Utils.Network.ResultHandles
{
    public abstract class Result
    {
    }

    public class SuccessfulResult<T> : Result where T : BaseModel
    {
        public readonly T Response;

        public SuccessfulResult(T response)
        {
            Response = response;
        }
    }

    public class ErrorResult : Result
    {
        public readonly NetworkError Error;

        public ErrorResult(NetworkError error)
        {
            Error = error;
        }
    }

    public abstract class NetworkError
    {
    }

    public class ConnectionError : NetworkError
    {
    }

    public class ResponseError : NetworkError
    {
        [JsonProperty] public readonly string message;
        [JsonProperty] public readonly int statusCode;
    }

    public class TypeError : NetworkError
    {
    }

    public class ServiceError : NetworkError
    {
        [JsonProperty] public readonly string response;
        [JsonProperty] public readonly int statusCode;
    }

    public abstract class BaseModel
    {
    }
}