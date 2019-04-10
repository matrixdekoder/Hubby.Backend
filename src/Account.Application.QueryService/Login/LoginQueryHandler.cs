using System;
using System.Threading;
using System.Threading.Tasks;
using Account.Application.QueryService.Account;
using Core.Application.Exceptions;
using Core.Infrastructure.Security;
using Library.Mongo.Persistence;
using MediatR;
using MongoDB.Driver;

namespace Account.Application.QueryService.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginQueryResponse>
    {
        private readonly IPasswordComputer _passwordComputer;
        private readonly ITokenHandler _tokenHandler;
        private readonly IMongoCollection<AccountReadModel> _collection;

        public LoginQueryHandler(IMongoContext context, IPasswordComputer passwordComputer, ITokenHandler tokenHandler)
        {
            _passwordComputer = passwordComputer;
            _tokenHandler = tokenHandler;
            _collection = context.GetCollection<AccountReadModel>();
        }

        public async Task<LoginQueryResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var view = await _collection.Find(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (view == null) throw new ItemNotFoundException($"Account with username {request.Id} not found");

            var isAuthorized = _passwordComputer.Compare(request.Password, view.Password);
            if (!isAuthorized) throw new UnauthorizedAccessException("Password incorrect.");
            
            var token = _tokenHandler.Create(request.Id);
            return new LoginQueryResponse(view.Id, view.BuddyId, token);
        }
    }
}