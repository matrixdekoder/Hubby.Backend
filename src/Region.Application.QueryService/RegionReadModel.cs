using System.Collections.Generic;
using Core.Application;
using Core.Application.Query;

namespace Region.Application.QueryService
{
    public class RegionReadModel: IReadModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<string> GroupIds { get; set; }
    }
}
