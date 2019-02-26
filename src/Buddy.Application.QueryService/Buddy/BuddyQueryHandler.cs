using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Library.Mongo;
using MediatR;
using MongoDB.Driver;

namespace Buddy.Application.QueryService.Buddy
{
    public class BuddyQueryHandler: IRequestHandler<BuddyQuery, IList<BuddyReadModel>>
    {
        private readonly IMongoCollection<BuddyReadModel> _collection;

        public BuddyQueryHandler(IMongoContext context)
        {
            _collection = context.GetCollection<BuddyReadModel>();
        }

        public async Task<IList<BuddyReadModel>> Handle(BuddyQuery request, CancellationToken cancellationToken)
        {
            return await _collection.Find(request.Predicate).ToListAsync(cancellationToken);
        }
    }
}
