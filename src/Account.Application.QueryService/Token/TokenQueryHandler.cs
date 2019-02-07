using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Account.Application.QueryService.Login;
using Core.Application;
using Core.Infrastructure.Security;
using Library.Mongo;
using MediatR;
using MongoDB.Driver;

namespace Account.Application.QueryService.Token
{
    public class TokenQueryHandler: IRequestHandler<TokenQueryModel, LoginTokenResponse>
    {
        private readonly IMongoCollection<TokenReadModel> _collection;
        private readonly ITokenHandler _tokenHandler;
        private readonly IProjectionWriter<TokenReadModel> _writer;

        public TokenQueryHandler(IMongoContext context, ITokenHandler tokenHandler, IProjectionWriter<TokenReadModel> writer)
        {
            _collection = context.GetCollection<TokenReadModel>();
            _tokenHandler = tokenHandler;
            _writer = writer;
        }
        
        public async Task<LoginTokenResponse> Handle(TokenQueryModel request, CancellationToken cancellationToken)
        {
            var principal = _tokenHandler.GetExpiredTokenClaimPrincipal(request.AccessToken);
            if(principal == null) throw new UnauthorizedAccessException("Invalid access token");

            var username = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            if(username == null) throw new UnauthorizedAccessException("User claim not found");

            var storedToken = await _collection.Find(x => x.Id == username).FirstOrDefaultAsync(cancellationToken);
            if(storedToken == null) throw new UnauthorizedAccessException($"No refresh token found for user {username}");

            if(_tokenHandler.IsTokenExpired(storedToken.RefreshToken)) 
                throw new UnauthorizedAccessException("Refresh token is expired");

            var newToken = _tokenHandler.Handle(username);
            await _writer.Update(username, x => x.RefreshToken = newToken.RefreshToken);
            
            return new LoginTokenResponse(username, newToken.AccessToken, newToken.RefreshToken);
        }
    }
}