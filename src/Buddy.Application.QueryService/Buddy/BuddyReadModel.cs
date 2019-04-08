using System.Collections.Generic;
using Buddy.Domain;
using Core.Application;

namespace Buddy.Application.QueryService.Buddy
{
    public class BuddyReadModel: IReadModel
    {
        public string Id { get; set; }
        public string RegionId { get; set; }
        public IList<string> GenreIds { get; set; }
        public string GroupId { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
