using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using MediatR;

namespace Buddy.Application.QueryService.Group.Listeners
{
    public class GroupStartedListener: INotificationHandler<GroupStarted>
    {
        private readonly IProjectionWriter<GroupReadModel> _writer;

        public GroupStartedListener(IProjectionWriter<GroupReadModel> writer)
        {
            _writer = writer;
        }

        public async Task Handle(GroupStarted notification, CancellationToken cancellationToken)
        {
            var view = new GroupReadModel
            {
                Id = notification.Id, 
                RegionId = notification.RegionId, 
                GenreIds = notification.GenreIds
            };

            await _writer.Add(view);
        }
    }
}
