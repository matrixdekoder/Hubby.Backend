using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using Core.Infrastructure.Security;
using MediatR;

namespace Account.Application.CommandService.Register
{
    public class RegisterAccountCommandHandler: INotificationHandler<RegisterAccountCommand>
    {
        private readonly IRepository<Domain.Account> _accountRepository;
        private readonly IPasswordComputer _passwordComputer;
        
        public RegisterAccountCommandHandler(IRepository<Domain.Account> accountRepository, IPasswordComputer passwordComputer)
        {
            _accountRepository = accountRepository;
            _passwordComputer = passwordComputer;
        }

        public async Task Handle(RegisterAccountCommand notification, CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordComputer.Hash(notification.Password);
            var account = await _accountRepository.GetById(notification.Id);
            account.Register(notification.Id, hashedPassword);
            await _accountRepository.Save(account);
        }
    }
}
