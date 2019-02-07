using System.Security.Claims;

namespace Core.Infrastructure.Security
{
    public interface ITokenHandler
    {
        TokenModel Create(string username);
        ClaimsPrincipal GetExpiredTokenClaimPrincipal(string token);
        bool IsTokenExpired(string token);
    }
}