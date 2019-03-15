using System.Threading;
using System.Threading.Tasks;
using Library.Mongo.Persistence;
using MediatR;
using MongoDB.Driver;

namespace Buddy.Application.QueryService.Group.Get
{
    public class GetGroupQueryModel: IRequestHandler<GetGroupQuery, GroupReadModel>
    {
        private readonly IMongoCollection<GroupReadModel> _collection;

        public GetGroupQueryModel(IMongoContext context)
        {
            _collection = context.GetCollection<GroupReadModel>();
        }

        public async Task<GroupReadModel> Handle(GetGroupQuery request, CancellationToken cancellationToken)
        {
            return await _collection.Find(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
