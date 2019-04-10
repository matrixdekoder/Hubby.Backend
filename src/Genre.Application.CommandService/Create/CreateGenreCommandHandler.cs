using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using Core.Domain.Entities;
using MediatR;

namespace Genre.Application.CommandService.Create
{
    public class CreateGenreCommandHandler: INotificationHandler<CreateGenreCommand>
    {
        private readonly IRepository _repository;

        public CreateGenreCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateGenreCommand notification, CancellationToken cancellationToken)
        {
            var genre = await _repository.GetById<Domain.Genre>(notification.Code);
            genre.Create(notification.Code, notification.Name);
            await _repository.Save(genre);
        }
    }
}
