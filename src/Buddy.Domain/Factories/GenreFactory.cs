using System.Collections.Generic;
using Buddy.Domain.Entities;
using Core.Domain;

namespace Buddy.Domain.Factories
{
    public class GenreFactory: IAggregateFactory<Genre>
    {
        public Genre Create(IList<IEvent> events)
        {
            return new Genre(events);
        }
    }
}
