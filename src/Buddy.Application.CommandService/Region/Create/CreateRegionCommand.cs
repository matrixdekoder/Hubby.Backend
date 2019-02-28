using MediatR;

namespace Buddy.Application.CommandService.Region.Create
{
    public class CreateRegionCommand: INotification
    {
        public CreateRegionCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
