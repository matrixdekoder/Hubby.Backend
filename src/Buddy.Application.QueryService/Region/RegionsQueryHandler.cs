using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Library.Mongo;
using MediatR;
using MongoDB.Driver;

namespace Buddy.Application.QueryService.Region
{
    public class RegionsQueryHandler: IRequestHandler<RegionsQuery, IList<Domain.Entities.Region>>
    {
        private readonly IMongoCollection<Domain.Entities.Region> _collection;
        
        public RegionsQueryHandler(IMongoContext context)
        {
            _collection = context.GetCollection<Domain.Entities.Region>();
        }

        public async Task<IList<Domain.Entities.Region>> Handle(RegionsQuery request, CancellationToken cancellationToken)
        {
            return await _collection.Find(FilterDefinition<Domain.Entities.Region>.Empty).ToListAsync(cancellationToken);
        }
    }
}
