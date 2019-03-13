using System.Threading;
using System.Threading.Tasks;
using Buddy.Application.QueryService.Buddy;
using Buddy.Domain.Events;
using Core.Application;
using MediatR;

namespace Buddy.Application.QueryService.Genre.Listeners
{
    public class GenresChosenListener: INotificationHandler<GenresChosen>
    {
        private readonly IProjectionWriter _writer;

        public GenresChosenListener(IProjectionWriter writer)
        {
            _writer = writer;
        }

        public async Task Handle(GenresChosen notification, CancellationToken cancellationToken)
        {
            await _writer.Update<BuddyReadModel>(notification.Id, view => view.GenreIds = notification.GenreIds);
        }
    }
}
