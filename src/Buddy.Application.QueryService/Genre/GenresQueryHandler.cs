using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Library.Mongo;
using MediatR;
using MongoDB.Driver;

namespace Buddy.Application.QueryService.Genre
{
    public class GenresQueryHandler: IRequestHandler<GenresQuery, IList<Domain.Entities.Genre>>
    {
        private readonly IMongoCollection<Domain.Entities.Genre> _collection;

        public GenresQueryHandler(IMongoContext context)
        {
            _collection = context.GetCollection<Domain.Entities.Genre>();
        }

        public async Task<IList<Domain.Entities.Genre>> Handle(GenresQuery request, CancellationToken cancellationToken)
        {
            return await _collection.Find(FilterDefinition<Domain.Entities.Genre>.Empty).ToListAsync(cancellationToken);
        }
    }
}
