using System.Collections.Generic;

namespace Buddy.Domain
{
    public class Genre
    {
        public Genre(string id, string name, IList<Genre> subGenres)
        {
            Id = id;
            Name = name;
            SubGenres = subGenres;
        }

        public string Id { get; }
        public string Name { get; }
        public IList<Genre> SubGenres { get; }
    }
}
