using System.Threading.Tasks;
using Buddy.Domain.Constants;
using Buddy.Domain.Events;
using Core.Application.Command;
using MediatR;
using TaskStatus = Buddy.Domain.Enums.TaskStatus;

namespace Buddy.Application.CommandService.Buddy.SetTaskStatus
{
    public class SetTaskStatusGenresChosenListener: CommandListener<GenresChosen>
    {
        public SetTaskStatusGenresChosenListener(IMediator mediator) : base(mediator)
        {
        }

        protected override async Task Handle(GenresChosen notification)
        {
            var command = new SetTaskStatusCommand(notification.Id, TaskType.ChooseGenres, TaskStatus.Completed);
            await Mediator.Publish(command);
        }
    }
}
