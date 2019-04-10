using Core.Domain;
using Core.Domain.Entities;

namespace Genre.Domain
{
    public class Genre: Aggregate<Genre>
    {
        public void Create(string id, string name)
        {
            if(Version != 0) return;
            var e = new GenreCreated(id, name);
            Publish(e);
        }

        private void When(GenreCreated e)
        {
            Id = e.Id;
        }
    }
}
