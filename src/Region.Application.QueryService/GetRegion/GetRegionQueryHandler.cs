using System.Threading;
using System.Threading.Tasks;
using Library.Mongo;
using MediatR;
using MongoDB.Driver;

namespace Region.Application.QueryService.GetRegion
{
    class GetRegionQueryHandler: IRequestHandler<GetRegionQuery, RegionReadModel>
    {
        private readonly IMongoCollection<RegionReadModel> _collection;

        public GetRegionQueryHandler(IMongoContext context)
        {
            _collection = context.GetCollection<RegionReadModel>();
        }

        public async Task<RegionReadModel> Handle(GetRegionQuery request, CancellationToken cancellationToken)
        {
            return await _collection.Find(x => x.Id == request.RegionId).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
