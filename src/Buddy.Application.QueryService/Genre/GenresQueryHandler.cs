using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Library.Mongo;
using MediatR;
using MongoDB.Driver;

namespace Buddy.Application.QueryService.Genre
{
    public class GenresQueryHandler: IRequestHandler<GenresQuery, IList<GenreReadModel>>
    {
        private readonly IMongoCollection<GenreReadModel> _collection;

        public GenresQueryHandler(IMongoContext context)
        {
            _collection = context.GetCollection<GenreReadModel>();
        }

        public async Task<IList<GenreReadModel>> Handle(GenresQuery request, CancellationToken cancellationToken)
        {
            return await _collection.Find(FilterDefinition<GenreReadModel>.Empty).ToListAsync(cancellationToken);
        }
    }
}
