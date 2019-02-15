﻿using System.Collections.Generic;
using Core.Domain;

namespace Buddy.Domain.Events
{
    public class GenresChosen: IEvent
    {
        public GenresChosen(string id, IList<string> genreIds)
        {
            Id = id;
            GenreIds = genreIds;
        }

        public string Id { get; }
        public IList<string> GenreIds { get; }
    }
}
