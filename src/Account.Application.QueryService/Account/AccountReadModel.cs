using Core.Application;

namespace Account.Application.QueryService.Account
{
    public class AccountReadModel: IReadModel
    {
        public string Id { get; set; }
        public string BuddyId { get; set; }
        public string Password { get; set; }
    }
}