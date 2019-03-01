using Core.Application;

namespace Buddy.Application.QueryService.Genre
{
    public class GenreReadModel : IReadModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
