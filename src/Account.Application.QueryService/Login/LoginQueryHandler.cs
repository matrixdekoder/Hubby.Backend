using System;
using System.Threading;
using System.Threading.Tasks;
using Account.Application.QueryService.Token;
using Core.Application;
using Core.Application.Exceptions;
using Core.Infrastructure.Security;
using Library.Mongo;
using MediatR;
using MongoDB.Driver;

namespace Account.Application.QueryService.Login
{
    public class LoginQueryHandler: IRequestHandler<LoginQueryModel, LoginTokenResponse>
    {
        private readonly IPasswordComputer _passwordComputer;
        private readonly ITokenHandler _tokenHandler;
        private readonly IMongoContext _context;
        private readonly IProjectionWriter<TokenReadModel> _writer;

        public LoginQueryHandler(
            IMongoContext context, 
            IPasswordComputer passwordComputer, 
            ITokenHandler tokenHandler, 
            IProjectionWriter<TokenReadModel> writer)
        {
            _passwordComputer = passwordComputer;
            _tokenHandler = tokenHandler;
            _context = context;
            _writer = writer;
        }
        
        public async Task<LoginTokenResponse> Handle(LoginQueryModel request, CancellationToken cancellationToken)
        {
            var view = await _context.GetCollection<LoginReadModel>().Find(x => x.Username == request.Username).FirstOrDefaultAsync(cancellationToken);
            if(view == null) throw new ItemNotFoundException($"Account with username {request.Username} not found");

            var isAuthorized = _passwordComputer.Compare(request.Password, view.Password);
            if(!isAuthorized) throw new UnauthorizedAccessException("Password incorrect.");

            var token = _tokenHandler.Handle(request.Username);
            await ParseRefreshToken(request.Username, token.RefreshToken);
            return new LoginTokenResponse(request.Username, token.AccessToken, token.RefreshToken);
        }

        private async Task ParseRefreshToken(string username, string refreshToken)
        {
            var view = await _context.GetCollection<TokenReadModel>().Find(x => x.Id == username).FirstOrDefaultAsync();
            if (view == null)
            {
                view = new TokenReadModel { Id = username, RefreshToken = refreshToken };
                await _writer.Add(view);
            }
            else
            {
                await _writer.Update(username, x => x.RefreshToken = refreshToken);
            }
        }
    }
}