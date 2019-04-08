using System.Threading.Tasks;
using Account.Domain;
using Core.Application;

namespace Account.Application.QueryService.Account
{
    public class AccountRegisteredListener: QueryListener<AccountRegistered>
    {
        public AccountRegisteredListener(IProjectionWriter writer): base(writer)
        {
        }

        protected override async Task Handle(AccountRegistered e)
        {
            var view = new AccountReadModel
            {
                Id = e.Id,
                Password = e.Password
            };

            await Writer.Add(view);
        }
    }
}
