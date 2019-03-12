using MediatR;

namespace Region.Application.CommandService.Create
{
    public class CreateRegionCommand: INotification
    {
        public CreateRegionCommand(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public string Code { get; }
        public string Name { get; }
    }
}
