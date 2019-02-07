using Account.Application.QueryService.Token;
using MediatR;

namespace Account.Application.QueryService.Login
{
    public class LoginQueryModel: IRequest<TokenResponseModel>
    {   
        public string Username { get; set; }
        public string Password { get; set; }
    }
}