using MediatR;

namespace Buddy.Application.CommandService.Buddy.ChooseRegion
{
    public class ChooseRegionCommand: INotification
    {
        public ChooseRegionCommand(string buddyId, string regionId)
        {
            BuddyId = buddyId;
            RegionId = regionId;
        }

        public string BuddyId { get; }
        public string RegionId { get; }
    }
}
