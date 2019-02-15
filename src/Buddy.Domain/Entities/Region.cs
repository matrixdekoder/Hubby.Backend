using Core.Domain;

namespace Buddy.Domain.Entities
{
    public class Region: IEntity
    {
        public Region(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; }
        public string Name { get; }
    }
}
