using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using MediatR;

namespace Buddy.Application.QueryService.Genre.Listeners
{
    public class GenreCreatedListener: INotificationHandler<GenreCreated>
    {
        private readonly IProjectionWriter<GenreReadModel> _writer;

        public GenreCreatedListener(IProjectionWriter<GenreReadModel> writer)
        {
            _writer = writer;
        }

        public async Task Handle(GenreCreated notification, CancellationToken cancellationToken)
        {
            var view = new GenreReadModel
            {
                Id = notification.Id, 
                Name = notification.Name
            };

            await _writer.Add(view);
        }
    }
}
