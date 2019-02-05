using System;
using System.Threading;
using System.Threading.Tasks;
using Account.Domain;
using Core.Infrastructure;
using Library.EventStore;
using MediatR;
using MongoDB.Driver;

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

            var aggregate = await _repository.GetById(notification.Username);
            aggregate.Register(notification.Username, notification.Username, hashedPassword);
            await _repository.Save(aggregate);
            return new RegisterAccountResponse(notification.Username, notification.Username);
        }
    }
}
