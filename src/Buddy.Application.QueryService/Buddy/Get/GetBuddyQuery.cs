using MediatR;

namespace Buddy.Application.QueryService.Buddy.Get
{
    public class GetBuddyQuery: IRequest<BuddyReadModel>
    {
        public GetBuddyQuery(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
