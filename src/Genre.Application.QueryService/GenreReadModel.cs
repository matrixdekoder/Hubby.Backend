using Core.Application;
using Core.Application.Query;

namespace Genre.Application.QueryService
{
    public class GenreReadModel : IReadModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
