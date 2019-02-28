using System.Collections.Generic;
using MediatR;

namespace Buddy.Application.CommandService.Group.Create
{
    public class CreateGroupCommand: INotification
    {
        public CreateGroupCommand(string id, string regionId, IList<string> genreIds)
        {
            Id = id;
            RegionId = regionId;
            GenreIds = genreIds;
        }

        public string Id { get; }
        public string RegionId { get; }
        public IList<string> GenreIds { get; }
    }
}
