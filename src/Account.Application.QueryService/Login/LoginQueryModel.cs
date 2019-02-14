using MediatR;

namespace Account.Application.QueryService.Login
{
    public class LoginQueryModel: IRequest<LoginQueryResponse>
    {   
        public string Id { get; set; }
        public string Password { get; set; }
    }
}