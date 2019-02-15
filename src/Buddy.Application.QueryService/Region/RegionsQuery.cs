using System.Collections.Generic;
using MediatR;

namespace Buddy.Application.QueryService.Region
{
    public class RegionsQuery: IRequest<IList<Domain.Entities.Region>>
    {
    }
}
