using System.Collections.Generic;
using Core.Domain;

namespace Genre.Domain
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
