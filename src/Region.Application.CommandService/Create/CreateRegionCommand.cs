using MediatR;

namespace Region.Application.CommandService.Create
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
