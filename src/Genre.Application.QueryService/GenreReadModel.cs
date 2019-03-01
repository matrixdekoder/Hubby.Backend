using Core.Application;

namespace Genre.Application.QueryService
{
    public class GenreReadModel : IReadModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
