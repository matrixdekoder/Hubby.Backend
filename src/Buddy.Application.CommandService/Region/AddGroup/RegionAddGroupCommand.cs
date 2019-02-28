using MediatR;

namespace Buddy.Application.CommandService.Region.AddGroup
{
    public class RegionAddGroupCommand: INotification
    {
        public RegionAddGroupCommand(string regionId, string groupId)
        {
            RegionId = regionId;
            GroupId = groupId;
        }

        public string RegionId { get; }
        public string GroupId { get; }
    }
}
