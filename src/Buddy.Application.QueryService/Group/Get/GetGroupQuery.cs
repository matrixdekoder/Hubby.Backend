using MediatR;

namespace Buddy.Application.QueryService.Group.Get
{
    public class GetGroupQuery: IRequest<GroupReadModel>
    {
        public GetGroupQuery(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
