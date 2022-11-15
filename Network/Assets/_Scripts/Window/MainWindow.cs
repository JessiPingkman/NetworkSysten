using _Scripts.Repository;
using _Scripts.Utils.Network.ResultHandles;
using UnityEngine;

namespace _Scripts.Window
{
    public class MainWindow : Utils.Window.Window
    {
        private TokenRepository _tokenRepository = new TokenRepository();

        private void Awake()
        {
        }

        public async void GetToken()
        {
            Result result = await _tokenRepository.GetToken();
            if (result is SuccessfulResult<TokenModel> successfulResult)
            {
                TokenModel model = successfulResult.Response;
                Debug.LogError(successfulResult.Response);
            }

            if (result is ErrorResult errorResult)
            {
                NetworkError error = errorResult.Error;
                switch (error)
                {
                    case TypeError:
                        break;
                }
            }
        }
    }
}