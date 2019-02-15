using System.Threading;
using System.Threading.Tasks;
using Library.EventStore;
using MediatR;

namespace Buddy.Application.CommandService.ChooseRegion
{
    public class ChooseRegionCommandHandler: INotificationHandler<ChooseRegionCommand>
    {
        private readonly IEventStoreRepository<Domain.Entities.Buddy> _repository;

        public ChooseRegionCommandHandler(IEventStoreRepository<Domain.Entities.Buddy> repository)
        {
            _repository = repository;
        }

        public async Task Handle(ChooseRegionCommand notification, CancellationToken cancellationToken)
        {
            var buddy = await _repository.GetById(notification.BuddyId);
            buddy.ChooseRegion(notification.RegionId);
            await _repository.Save(buddy);
        }
    }
}
