using Account.Application.QueryService.Login;
using MediatR;

namespace Account.Application.QueryService.Token
{
    public class TokenQueryModel : IRequest<LoginTokenResponse>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}