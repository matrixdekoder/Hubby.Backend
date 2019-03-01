using System.Collections.Generic;
using Core.Application;

namespace Region.Application.QueryService
{
    public class RegionReadModel: IReadModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<string> GroupIds { get; set; }
    }
}
