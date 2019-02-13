using MediatR;

namespace Account.Application.QueryService.Login
{
    public class LoginQueryModel: IRequest<LoginTokenResponse>
    {   
        public string Id { get; set; }
        public string Password { get; set; }
    }
}