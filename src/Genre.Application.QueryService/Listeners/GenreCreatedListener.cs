using System.Threading.Tasks;
using Core.Application;
using Genre.Domain;

namespace Genre.Application.QueryService.Listeners
{
    public class GenreCreatedListener : QueryListener<GenreCreated>
    {
        public GenreCreatedListener(IProjectionWriter writer) : base(writer)
        {
        }

        protected override async Task Handle(GenreCreated notification)
        {
            var view = new GenreReadModel
            {
                Id = notification.Id,
                Name = notification.Name
            };

            await Writer.Add(view);
        }
    }
}
