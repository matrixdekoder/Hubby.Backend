using System;
using System.Threading;
using System.Threading.Tasks;
using Account.Domain;
using Core.Infrastructure;
using Library.EventStore;
using Library.Mongo;
using MediatR;
using MongoDB.Driver;

namespace Account.Application.CommandService.Register
{
    public class RegisterAccountHandler: IRequestHandler<RegisterAccount, RegisterAccountResponse>
    {
        private readonly IEventStoreRepository<Domain.Account> _repository;
        private readonly IPasswordComputer _passwordComputer;
        private readonly IMongoCollection<Login> _collection;

        public RegisterAccountHandler(IEventStoreRepository<Domain.Account> repository, IPasswordComputer passwordComputer, IMongoContext context)
        {
            _repository = repository;
            _passwordComputer = passwordComputer;
            _collection = context.GetCollection<Domain.Login>();
        }

        public async Task<RegisterAccountResponse> Handle(RegisterAccount notification, CancellationToken cancellationToken)
        {
            var account = await _collection.Find(x => x.Username == notification.Username).FirstOrDefaultAsync(cancellationToken);
            if (account != null) throw new InvalidOperationException($"Account with username {notification.Username} already exists");

            var id = Guid.NewGuid();
            var hashedPassword = _passwordComputer.Hash(notification.Password);

            var aggregate = await _repository.GetById(id);
            aggregate.Register(id, notification.Username, hashedPassword);
            await _repository.Save(aggregate);
            return new RegisterAccountResponse(id, notification.Username);
        }
    }
}
