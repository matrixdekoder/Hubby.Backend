using MediatR;

namespace Genre.Application.CommandService.Create
{
    public class CreateGenreCommand: INotification
    {
        public CreateGenreCommand(string code, string name)
        {
            Code = code;
            Name = name;
        }


        public string Code { get; }
        public string Name { get; }
    }
}
