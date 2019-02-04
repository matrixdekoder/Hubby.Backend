using System.Threading;
using System.Threading.Tasks;
using Account.Domain;
using Core.Application;
using MediatR;

namespace Account.Application.QueryService.Login
{
    public class LoginReadHandler: INotificationHandler<AccountRegistered>
    {
        private readonly IProjectionWriter<Domain.Login> _writer;

        public LoginReadHandler(IProjectionWriter<Domain.Login> writer)
        {
            _writer = writer;
        }

        public async Task Handle(AccountRegistered notification, CancellationToken cancellationToken)
        {
            var view = new Domain.Login
            {
                Id = notification.Id, 
                Username = notification.Username, 
                Password = notification.Password
            };

            await _writer.Add(view);
        }
    }
}
