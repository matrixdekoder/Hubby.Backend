using Core.Domain;

namespace Genre.Domain
{
    public class Genre: Aggregate<Genre>
    {
        public void Create(string id, string name)
        {
            var e = new GenreCreated(id, name);
            Publish(e);
        }

        private void When(GenreCreated e)
        {
            Id = e.Id;
        }
    }
}
