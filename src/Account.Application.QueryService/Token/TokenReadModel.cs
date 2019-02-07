using Core.Domain;

namespace Account.Application.QueryService.Token
{
    public class TokenReadModel: IEntity
    {
        public string Id { get; set; }
        public string RefreshToken { get; set; }
    }
}