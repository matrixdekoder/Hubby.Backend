using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Exceptions;
using Core.Infrastructure.Security;
using Library.Mongo;
using MediatR;
using MongoDB.Driver;

namespace Account.Application.QueryService.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQueryModel, LoginTokenResponse>
    {
        private readonly IPasswordComputer _passwordComputer;
        private readonly ITokenHandler _tokenHandler;
        private readonly IMongoContext _context;

        public LoginQueryHandler(IMongoContext context, IPasswordComputer passwordComputer, ITokenHandler tokenHandler)
        {
            _passwordComputer = passwordComputer;
            _tokenHandler = tokenHandler;
            _context = context;
        }

        public async Task<LoginTokenResponse> Handle(LoginQueryModel request, CancellationToken cancellationToken)
        {
            var view = await _context.GetCollection<LoginReadModel>().Find(x => x.Username == request.Username).FirstOrDefaultAsync(cancellationToken);
            if (view == null) throw new ItemNotFoundException($"Account with username {request.Username} not found");

            var isAuthorized = _passwordComputer.Compare(request.Password, view.Password);
            if (!isAuthorized) throw new UnauthorizedAccessException("Password incorrect.");
            
            var token = _tokenHandler.Create(request.Username);
            return new LoginTokenResponse(request.Username, token);
        }
    }
}