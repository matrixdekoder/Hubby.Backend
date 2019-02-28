using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.ChooseGenres
{
    public class ChooseGenresCommandHandler: INotificationHandler<ChooseGenresCommand>
    {
        private readonly IRepository<Domain.Entities.Buddy> _repository;

        public ChooseGenresCommandHandler(IRepository<Domain.Entities.Buddy> repository)
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
