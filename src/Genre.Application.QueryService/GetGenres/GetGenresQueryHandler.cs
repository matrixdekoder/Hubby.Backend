using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Library.Mongo;
using MediatR;
using MongoDB.Driver;

namespace Genre.Application.QueryService.GetGenres
{
    public class GetGenresQueryHandler: IRequestHandler<GetGenresQuery, IList<GenreReadModel>>
    {
        private readonly IMongoCollection<GenreReadModel> _collection;

        public GetGenresQueryHandler(IMongoContext context)
        {
            _collection = context.GetCollection<GenreReadModel>();
        }

        public async Task<IList<GenreReadModel>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
        {
            return await _collection.Find(FilterDefinition<GenreReadModel>.Empty).ToListAsync(cancellationToken);
        }
    }
}
