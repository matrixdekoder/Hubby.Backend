using Core.Domain;

namespace Buddy.Domain.Events
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
