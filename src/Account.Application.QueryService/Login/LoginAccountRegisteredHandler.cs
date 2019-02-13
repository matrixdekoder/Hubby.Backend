using System.Threading;
using System.Threading.Tasks;
using Account.Domain;
using Core.Application;
using MediatR;

namespace Account.Application.QueryService.Login
{
    public class LoginAccountRegisteredHandler: INotificationHandler<AccountRegistered>
    {
        private readonly IProjectionWriter<LoginReadModel> _writer;

        public LoginAccountRegisteredHandler(IProjectionWriter<LoginReadModel> writer)
        {
            _writer = writer;
        }

        public async Task Handle(AccountRegistered notification, CancellationToken cancellationToken)
        {
            var view = new LoginReadModel
            {
                Id = notification.Id, 
                Password = notification.Password
            };

            await _writer.Add(view);
        }
    }
}
