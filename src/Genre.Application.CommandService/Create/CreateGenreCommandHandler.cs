using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;

namespace Genre.Application.CommandService.Create
{
    public class CreateGenreCommandHandler: INotificationHandler<CreateGenreCommand>
    {
        private readonly IRepository<Domain.Genre> _repository;

        public CreateGenreCommandHandler(IRepository<Domain.Genre> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateGenreCommand notification, CancellationToken cancellationToken)
        {
            var genreId = Guid.NewGuid().ToString();
            var genre = await _repository.GetById(genreId);
            genre.Create(genreId, notification.Name);
            await _repository.Save(genre);
        }
    }
}
