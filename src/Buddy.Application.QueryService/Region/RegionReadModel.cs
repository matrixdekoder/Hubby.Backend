using System.Collections.Generic;
using Core.Domain;

namespace Buddy.Application.QueryService.Region
{
    public class RegionReadModel: IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<string> GroupIds { get; set; }
    }
}
