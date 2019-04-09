using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using Core.Infrastructure.Security;
using MediatR;

namespace Account.Application.CommandService.Register
{
    public class RegisterAccountCommandHandler: INotificationHandler<RegisterAccountCommand>
    {
        private readonly ISagaRepository _repository;
        private readonly IPasswordComputer _passwordComputer;
        
        public RegisterAccountCommandHandler(ISagaRepository repository, IPasswordComputer passwordComputer)
        {
            _repository = repository;
            _passwordComputer = passwordComputer;
        }

        public async Task Handle(RegisterAccountCommand notification, CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordComputer.Hash(notification.Password);
            var account = await _repository.GetById<Domain.Account>(notification.Id);
            account.Register(notification.Id, hashedPassword);
            await _repository.Save(notification.TransactionId, account);
        }
    }
}
