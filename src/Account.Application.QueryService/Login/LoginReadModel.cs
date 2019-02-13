using Core.Domain;

namespace Account.Application.QueryService.Login
{
    public class LoginReadModel: IEntity
    {
        public string Id { get; set; }
        public string Password { get; set; }
    }
}