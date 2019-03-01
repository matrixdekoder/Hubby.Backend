using MediatR;

namespace Genre.Application.CommandService.Create
{
    public class CreateGenreCommand: INotification
    {
        public CreateGenreCommand(string name)
        {
            Name = name;
        }


        public string Name { get; }
    }
}
