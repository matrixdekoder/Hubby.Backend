using MediatR;

namespace Region.Application.QueryService.GetRegion
{
    public class GetRegionQuery: IRequest<RegionReadModel>
    {
        public GetRegionQuery(string regionId)
        {
            RegionId = regionId;
        }

        public string RegionId { get; }
    }
}
