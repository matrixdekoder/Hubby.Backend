using System.Threading;
using System.Threading.Tasks;
using Library.Mongo.Persistence;
using MediatR;
using MongoDB.Driver;

namespace Buddy.Application.QueryService.Buddy.Get
{
    public class GetBuddyQueryHandler: IRequestHandler<GetBuddyQuery, BuddyReadModel>
    {
        private readonly IMongoCollection<BuddyReadModel> _collection;

        public GetBuddyQueryHandler(IMongoContext context)
        {
            _collection = context.GetCollection<BuddyReadModel>();
        }

        public async Task<BuddyReadModel> Handle(GetBuddyQuery request, CancellationToken cancellationToken)
        {
            return await _collection.Find(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
