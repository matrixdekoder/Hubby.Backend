using System.Collections.Generic;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using Core.Application.Query;

namespace Buddy.Application.QueryService.Group.Listeners
{
    public class GroupCreatedListener : QueryListener<GroupCreated>
    {
        public GroupCreatedListener(IProjectionWriter writer) : base(writer)
        {
        }

        protected override async Task Handle(GroupCreated notification)
        {
            var view = new GroupReadModel
            {
                Id = notification.Id,
                RegionId = notification.RegionId,
                GenreIds = notification.GenreIds,
                BuddyIds = new List<string>()
            };

            await Writer.Add(view);
        }
    }
}
