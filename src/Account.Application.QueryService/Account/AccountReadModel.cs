using Core.Domain;

namespace Account.Application.QueryService.Account
{
    public class AccountReadModel: IEntity
    {
        public string Id { get; set; }
        public string BuddyId { get; set; }
        public string Password { get; set; }
    }
}