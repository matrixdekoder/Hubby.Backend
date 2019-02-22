using System.Threading;
using System.Threading.Tasks;
using Buddy.Application.QueryService.Buddy;
using Buddy.Domain.Enums;
using Core.Application.Exceptions;
using Library.Mongo;
using MediatR;
using MongoDB.Driver;

namespace Buddy.Application.QueryService.Status
{
    public class BuddyStatusQueryHandler: IRequestHandler<BuddyStatusQuery, BuddyStatus>
    {
        private readonly IMongoCollection<BuddyReadModel> _collection;

        public BuddyStatusQueryHandler(IMongoContext context)
        {
            _collection = context.GetCollection<BuddyReadModel>();
        }

        public async Task<BuddyStatus> Handle(BuddyStatusQuery request, CancellationToken cancellationToken)
        {
            var buddy = await _collection.Find(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if(buddy == null) throw new ItemNotFoundException($"Buddy {request.Id} not found");
            return buddy.Status;
        }
    }
}
