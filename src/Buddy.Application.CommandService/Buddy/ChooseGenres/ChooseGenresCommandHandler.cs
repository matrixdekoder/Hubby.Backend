using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Library.EventStore;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.ChooseGenres
{
    public class ChooseGenresCommandHandler: INotificationHandler<ChooseGenresCommand>
    {
        private readonly IEventStoreRepository<Domain.Entities.Buddy> _repository;

        public ChooseGenresCommandHandler(IEventStoreRepository<Domain.Entities.Buddy> repository)
        {
            _repository = repository;
        }

        public async Task Handle(ChooseGenresCommand notification, CancellationToken cancellationToken)
        {
            var buddy = await _repository.GetById(notification.BuddyId);
            buddy.ChooseGenres(notification.GenreIds.ToList());
            await _repository.Save(buddy);
        }
    }
}
