using Core.Domain;

namespace Genre.Domain
{
    public class GenreCreated: IEvent
    {
        public GenreCreated(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; }
        public string Name { get; }
    }
}
