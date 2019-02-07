using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Account.Application.QueryService.Token;
using Core.Infrastructure.Security;
using Library.Mongo;
using MediatR;
using MongoDB.Driver;

namespace Account.Application.QueryService.Refresh
{
    public class RefreshTokenQueryHandler: IRequestHandler<RefreshTokenQueryModel, TokenResponseModel>
    {
        private readonly IMongoCollection<TokenReadModel> _collection;
        private readonly ITokenHandler _tokenHandler;
        private readonly IMediator _mediator;

        public RefreshTokenQueryHandler(IMongoContext context, ITokenHandler tokenHandler, IMediator mediator)
        {
            _collection = context.GetCollection<TokenReadModel>();
            _tokenHandler = tokenHandler;
            _mediator = mediator;
        }
        
        public async Task<TokenResponseModel> Handle(RefreshTokenQueryModel request, CancellationToken cancellationToken)
        {
            var principal = _tokenHandler.GetExpiredTokenClaimPrincipal(request.AccessToken);
            if(principal == null) throw new UnauthorizedAccessException("Invalid access token");

            var username = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            if(username == null) throw new UnauthorizedAccessException("User claim not found");

            var storedToken = await _collection.Find(x => x.Id == username).FirstOrDefaultAsync(cancellationToken);
            if(storedToken == null) throw new UnauthorizedAccessException($"No refresh token found for user {username}");
            
            if(request.RefreshToken != storedToken.RefreshToken)
                throw new UnauthorizedAccessException("Invalid refresh token");

            if(_tokenHandler.IsTokenExpired(storedToken.RefreshToken)) 
                throw new UnauthorizedAccessException("Refresh token is expired");
            
            var notification = new TokenQueryModel(username, true);
            var token = await _mediator.Send(notification, cancellationToken);
            return token;
        }
    }
}