using System.Threading;
using System.Threading.Tasks;
using Core.Application;
using Genre.Domain;
using MediatR;

namespace Genre.Application.QueryService.Listeners
{
    public class GenreCreatedListener: INotificationHandler<GenreCreated>
    {
        private readonly IProjectionWriter _writer;

        public GenreCreatedListener(IProjectionWriter writer)
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
