using System.Collections.Generic;
using Core.Domain;

namespace Genre.Domain
{
    public class GenreFactory: IAggregateFactory<Genre>
    {
        public Genre Create(IList<IEvent> events)
        {
            return new Genre(events);
        }
    }
}
