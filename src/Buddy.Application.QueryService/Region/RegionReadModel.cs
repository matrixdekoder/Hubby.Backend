using System.Collections.Generic;
using Core.Application;

namespace Buddy.Application.QueryService.Region
{
    public class RegionReadModel: IReadModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<string> GroupIds { get; set; }
    }
}
