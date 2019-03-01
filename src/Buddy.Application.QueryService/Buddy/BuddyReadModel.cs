using System.Collections.Generic;
using Buddy.Domain.Enums;
using Core.Domain;

namespace Buddy.Application.QueryService.Buddy
{
    public class BuddyReadModel: IEntity
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string RegionId { get; set; }
        public IList<string> GenreIds { get; set; }
        public BuddyStatus Status { get; set; }
        public string GroupId { get; set; }
    }
}
