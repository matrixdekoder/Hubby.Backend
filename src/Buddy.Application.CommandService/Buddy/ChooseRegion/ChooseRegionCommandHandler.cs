using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using Core.Domain.Entities;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.ChooseRegion
{
    public class ChooseRegionCommandHandler: INotificationHandler<ChooseRegionCommand>
    {
        private readonly IRepository _repository;

        public ChooseRegionCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(ChooseRegionCommand notification, CancellationToken cancellationToken)
        {
            var buddy = await _repository.GetById<Domain.Buddy>(notification.BuddyId);
            buddy.ChooseRegion(notification.RegionId);
            await _repository.Save(buddy);
        }
    }
}
