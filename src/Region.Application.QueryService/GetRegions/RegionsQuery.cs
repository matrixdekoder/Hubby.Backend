using System.Collections.Generic;
using MediatR;

namespace Region.Application.QueryService.GetRegions
{
    public class RegionsQuery: IRequest<IList<RegionReadModel>>
    {
    }
}
