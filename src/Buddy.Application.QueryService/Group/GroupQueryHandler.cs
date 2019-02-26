using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Library.Mongo;
using MediatR;
using MongoDB.Driver;

namespace Buddy.Application.QueryService.Group
{
    public class GroupQueryHandler: IRequestHandler<GroupQuery, IList<GroupReadModel>>
    {
        private readonly IMongoCollection<GroupReadModel> _collection;

        public GroupQueryHandler(IMongoContext context)
        {
            _collection = context.GetCollection<GroupReadModel>();
        }

        public async Task<IList<GroupReadModel>> Handle(GroupQuery request, CancellationToken cancellationToken)
        {
            return await _collection.Find(request.Predicate).ToListAsync(cancellationToken);
        }
    }
}
