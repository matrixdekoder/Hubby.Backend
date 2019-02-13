using System.Threading;
using System.Threading.Tasks;
using Core.Infrastructure.Security;
using Library.EventStore;
using MediatR;

namespace Account.Application.CommandService.Register
{
    public class RegisterAccountHandler: IRequestHandler<RegisterAccount, RegisterAccountResponse>
    {
        private readonly IEventStoreRepository<Domain.Account> _repository;
        private readonly IPasswordComputer _passwordComputer;
        
        public RegisterAccountHandler(IEventStoreRepository<Domain.Account> repository, IPasswordComputer passwordComputer)
        {
            _repository = repository;
            _passwordComputer = passwordComputer;
        }

        public async Task<RegisterAccountResponse> Handle(RegisterAccount notification, CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordComputer.Hash(notification.Password);

            var aggregate = await _repository.GetById(notification.Id);
            aggregate.Register(notification.Id, hashedPassword);
            await _repository.Save(aggregate);
            return new RegisterAccountResponse(notification.Id);
        }
    }
}
