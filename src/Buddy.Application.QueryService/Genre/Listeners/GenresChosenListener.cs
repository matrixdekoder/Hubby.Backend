using System.Threading.Tasks;
using Buddy.Application.QueryService.Buddy;
using Buddy.Domain.Events;
using Core.Application;

namespace Buddy.Application.QueryService.Genre.Listeners
{
    public class GenresChosenListener : QueryListener<GenresChosen>
    {
        public GenresChosenListener(IProjectionWriter writer) : base(writer)
        {
        }

        protected override async Task Handle(GenresChosen notification)
        {
            await Writer.Update<BuddyReadModel>(notification.Id, view => view.GenreIds = notification.GenreIds);
        }
    }
}
