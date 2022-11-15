using System.Collections.Generic;
using System.Threading.Tasks;
using _Scripts.Utils.Network;
using _Scripts.Utils.Network.ResultHandles;

namespace _Scripts.Repository
{
    public abstract class Repository
    {
    }

    public class TokenRepository : Repository
    {
        public async Task<Result> GetToken()
        {
            return await Network.Get<TokenModel>(NetworgAPISSettings.RefreshToken, new List<HeaderOption>
            {
                new HeaderOption
                {
                    Option = ("Content-Type", "application/json")
                },
                new HeaderOption
                {
                    Option = ("Authorization", "Bear")
                }
            });
        }
    }

    public class TokenModel : BaseModel
    {
    }
}