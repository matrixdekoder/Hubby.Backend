using Core.Domain;

namespace Buddy.Domain
{
    public class Region: IEntity
    {
        public Region(string name)
        {
            Name = name;
        }

        public string Id { get; set; }
        public string Name { get; }
    }
}
