using System;
using System.Collections.Generic;
using Buddy.Domain.Events;
using Core.Domain;

namespace Buddy.Domain.Entities
{
    public class Buddy: Aggregate<Buddy>
    {
        private const int GenresAmount = 5;
        private string _regionId;
        private IList<string> _genreIds;

        public Buddy(IEnumerable<IEvent> events) : base(events)
        {
        }

        public void Create(string buddyId)
        {
            var e = new BuddyCreated(buddyId);
            Publish(e);
        }

        public void ChooseRegion(string regionId)
        {
            var e = new RegionChosen(Id, regionId);
            Publish(e);
        }

        public void ChooseGenres(IList<string> genreIds)
        {
            if(genreIds.Count != GenresAmount)
                throw new InvalidOperationException($"You have to pick exactly {GenresAmount} genres");

            var e = new GenresChosen(Id, genreIds);
            Publish(e);
        }

        private void When(BuddyCreated e)
        {
            Id = e.Id;
        }

        private void When(RegionChosen e)
        {
            _regionId = e.RegionId;
        }

        public void When(GenresChosen e)
        {
            _genreIds = e.GenreIds;
        }
    }
}
