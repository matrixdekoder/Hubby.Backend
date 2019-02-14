using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Library.Mongo;
using MediatR;
using MongoDB.Driver;

namespace Buddy.Application.QueryService.Region
{
    public class RegionsQueryHandler: IRequestHandler<RegionsQuery, IList<Domain.Region>>
    {
        private readonly IMongoCollection<Domain.Region> _collection;
        
        public RegionsQueryHandler(IMongoContext context)
        {
            _collection = context.GetCollection<Domain.Region>();
        }

        public async Task<IList<Domain.Region>> Handle(RegionsQuery request, CancellationToken cancellationToken)
        {
            return await _collection.Find(FilterDefinition<Domain.Region>.Empty).ToListAsync(cancellationToken);
        }
    }
}
