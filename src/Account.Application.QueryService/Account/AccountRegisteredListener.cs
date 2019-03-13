using System.Threading;
using System.Threading.Tasks;
using Account.Domain;
using Core.Application;
using MediatR;

namespace Account.Application.QueryService.Account
{
    public class AccountRegisteredListener: INotificationHandler<AccountRegistered>
    {
        private readonly IProjectionWriter _writer;

        public AccountRegisteredListener(IProjectionWriter writer)
        {
            _writer = writer;
        }

        public async Task Handle(AccountRegistered notification, CancellationToken cancellationToken)
        {
            var view = new AccountReadModel
            {
                Id = notification.Id, 
                Password = notification.Password
            };

            await _writer.Add(view);
        }
    }
}
