using MediatR;

namespace Account.Application.QueryService.Token
{
    public class TokenQueryModel: IRequest<TokenResponseModel>
    {
        public TokenQueryModel(string username, bool isRefresh)
        {
            Username = username;
            IsRefresh = isRefresh;
        }
        
        public string Username { get; }
        public bool IsRefresh { get; }
    }
}