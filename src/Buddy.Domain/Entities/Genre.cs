using System.Collections.Generic;
using Buddy.Domain.Events;
using Core.Domain;

namespace Buddy.Domain.Entities
{
    public class Genre: Aggregate<Genre>
    {
        private string _name;

        public Genre(IEnumerable<IEvent> events) : base(events)
        {
        }

        public void Create(string id, string name)
        {
            var e = new GenreCreated(id, name);
            Publish(e);
        }

        private void When(GenreCreated e)
        {
            Id = e.Id;
            _name = e.Name;
        }
    }
}
