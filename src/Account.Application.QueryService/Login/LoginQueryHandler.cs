using System;
using System.Threading;
using System.Threading.Tasks;
using Account.Application.QueryService.Token;
using Core.Application.Exceptions;
using Core.Infrastructure.Security;
using Library.Mongo;
using MediatR;
using MongoDB.Driver;

namespace Account.Application.QueryService.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQueryModel, TokenResponseModel>
    {
        private readonly IPasswordComputer _passwordComputer;
        private readonly IMediator _mediator;
        private readonly IMongoContext _context;

        public LoginQueryHandler(IMongoContext context, IPasswordComputer passwordComputer, IMediator mediator)
        {
            _passwordComputer = passwordComputer;
            _mediator = mediator;
            _context = context;
        }

        public async Task<TokenResponseModel> Handle(LoginQueryModel request, CancellationToken cancellationToken)
        {
            var view = await _context.GetCollection<LoginReadModel>().Find(x => x.Username == request.Username)
                .FirstOrDefaultAsync(cancellationToken);
            if (view == null) throw new ItemNotFoundException($"Account with username {request.Username} not found");

            var isAuthorized = _passwordComputer.Compare(request.Password, view.Password);
            if (!isAuthorized) throw new UnauthorizedAccessException("Password incorrect.");

            var notification = new TokenQueryModel(request.Username, false);
            var token = await _mediator.Send(notification, cancellationToken);
            return token;
        }
    }
}