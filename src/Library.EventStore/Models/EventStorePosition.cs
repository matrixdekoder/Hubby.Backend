using Core.Domain;

namespace Library.EventStore.Models
{
    public class EventStorePosition: IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long CommitPosition { get; set; }
        public long PreparedPosition { get; set; }
    }
}
