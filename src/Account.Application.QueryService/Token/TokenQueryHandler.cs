using System.Threading;
using System.Threading.Tasks;
using Core.Application;
using Core.Infrastructure.Security;
using Library.Mongo;
using MediatR;
using MongoDB.Driver;

namespace Account.Application.QueryService.Token
{
    public class TokenQueryHandler: IRequestHandler<TokenQueryModel, TokenResponseModel>
    {
        private readonly IProjectionWriter<TokenReadModel> _writer;
        private readonly ITokenHandler _tokenHandler;
        private readonly IMongoCollection<TokenReadModel> _collection;

        public TokenQueryHandler(IProjectionWriter<TokenReadModel> writer, IMongoContext context, ITokenHandler tokenHandler)
        {
            _writer = writer;
            _tokenHandler = tokenHandler;
            _collection = context.GetCollection<TokenReadModel>();
        }
        
        public async Task<TokenResponseModel> Handle(TokenQueryModel request, CancellationToken cancellationToken)
        {
            var token = _tokenHandler.Create(request.Username);
            var view = await _collection.Find(x => x.Id == request.Username).FirstOrDefaultAsync(cancellationToken);
            
            if (view == null && !request.IsRefresh)
            {
                await _writer.Add(new TokenReadModel { Id = request.Username, RefreshToken = token.RefreshToken });
            }
            else
            {
                await _writer.Update(request.Username, x => x.RefreshToken = token.RefreshToken);
            }
            
            return new TokenResponseModel(request.Username, token.AccessToken, token.RefreshToken);
        }
    }
}