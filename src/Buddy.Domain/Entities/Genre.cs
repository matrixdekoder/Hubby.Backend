namespace Buddy.Domain.Entities
{
    public class Genre
    {
        public Genre(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; }
        public string Name { get; }
    }
}
