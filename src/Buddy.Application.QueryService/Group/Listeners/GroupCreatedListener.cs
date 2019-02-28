using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using MediatR;

namespace Buddy.Application.QueryService.Group.Listeners
{
    public class GroupCreatedListener: INotificationHandler<GroupCreated>
    {
        private readonly IProjectionWriter<GroupReadModel> _writer;

        public GroupCreatedListener(IProjectionWriter<GroupReadModel> writer)
        {
            _writer = writer;
        }

        public async Task Handle(GroupCreated notification, CancellationToken cancellationToken)
        {
            var view = new GroupReadModel
            {
                Id = notification.Id, 
                RegionId = notification.RegionId, 
                GenreIds = notification.GenreIds, 
                BuddyIds = new List<string>()
            };

            await _writer.Add(view);
        }
    }
}
