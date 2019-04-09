using System.Collections.Generic;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using Core.Application.Query;

namespace Buddy.Application.QueryService.Buddy.Listeners
{
    public class BuddyCreatedListener : QueryListener<BuddyCreated>
    {
        public BuddyCreatedListener(IProjectionWriter writer) : base(writer)
        {
        }

        protected override async Task Handle(BuddyCreated notification)
        {
            var view = new BuddyReadModel
            {
                Id = notification.Id,
                GenreIds = new List<string>(),
                Tasks = new List<Domain.Task>()
            };

            await Writer.Add(view);
        }
    }
}
