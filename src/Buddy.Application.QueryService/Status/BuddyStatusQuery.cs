using Buddy.Domain.Enums;
using MediatR;

namespace Buddy.Application.QueryService.Status
{
    public class BuddyStatusQuery: IRequest<BuddyStatus>
    {
        public BuddyStatusQuery(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
