using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using MediatR;

namespace Buddy.Application.QueryService.Buddy.Listeners
{
    public class GroupLeftListener: QueryListener<GroupLeft>
    {
        public GroupLeftListener(IProjectionWriter writer): base(writer)
        {
        }

        protected override async Task Handle(GroupLeft notification)
        {
            await Writer.Update<BuddyReadModel>(notification.Id, view => view.GroupId = null);
        }
    }
}
