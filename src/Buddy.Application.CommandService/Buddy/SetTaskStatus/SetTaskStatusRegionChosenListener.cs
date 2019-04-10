using System.Threading.Tasks;
using Buddy.Domain.Constants;
using Buddy.Domain.Events;
using Core.Application.Command;
using MediatR;
using TaskStatus = Buddy.Domain.Enums.TaskStatus;

namespace Buddy.Application.CommandService.Buddy.SetTaskStatus
{
    public class SetTaskStatusRegionChosenListener: CommandListener<RegionChosen>
    {
        public SetTaskStatusRegionChosenListener(IMediator mediator) : base(mediator)
        {
        }

        protected override async Task Handle(RegionChosen notification)
        {
            var command = new SetTaskStatusCommand(notification.Id, TaskType.ChooseRegion, TaskStatus.Completed);
            await Mediator.Publish(command);
        }
    }
}
