using Account.Application.QueryService.Token;
using MediatR;

namespace Account.Application.QueryService.Refresh
{
    public class RefreshTokenQueryModel : IRequest<TokenResponseModel>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}