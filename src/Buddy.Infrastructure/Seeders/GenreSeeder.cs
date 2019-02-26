using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Buddy.Domain.Entities;
using Core.Infrastructure;
using Library.Mongo;
using MongoDB.Driver;

namespace Buddy.Infrastructure.Seeders
{
    public class GenreSeeder : ISeeder
    {
        private readonly IMongoCollection<Genre> _collection;

        public GenreSeeder(IMongoContext context)
        {
            _collection = context.GetCollection<Genre>();
        }

        public async Task Seed()
        {
            var genres = new List<Genre>
            {
                new Genre(Guid.NewGuid().ToString(), "Platform"),
                new Genre(Guid.NewGuid().ToString(), "Shooter"),
                new Genre(Guid.NewGuid().ToString(), "Fighting"),
                new Genre(Guid.NewGuid().ToString(), "Beat 'm up"),
                new Genre(Guid.NewGuid().ToString(), "Stealth"),
                new Genre(Guid.NewGuid().ToString(), "Survival"),
                new Genre(Guid.NewGuid().ToString(), "Rhythm"),
                new Genre(Guid.NewGuid().ToString(), "Survival horror"),
                new Genre(Guid.NewGuid().ToString(), "Action RPG"),
                new Genre(Guid.NewGuid().ToString(), "MMORPG"),
                new Genre(Guid.NewGuid().ToString(), "Roguelikes"),
                new Genre(Guid.NewGuid().ToString(), "Tactical RPG"),
                new Genre(Guid.NewGuid().ToString(), "Sandbox RPG"),
                new Genre(Guid.NewGuid().ToString(), "Fantasy"),
                new Genre(Guid.NewGuid().ToString(), "Choices"),
                new Genre(Guid.NewGuid().ToString(), "Construction and Management simulation"),
                new Genre(Guid.NewGuid().ToString(), "Life simulation"),
                new Genre(Guid.NewGuid().ToString(), "Vehicle simulation"),
                new Genre(Guid.NewGuid().ToString(), "4X Strategy"),
                new Genre(Guid.NewGuid().ToString(), "Real Time Strategy (RTS)"),
                new Genre(Guid.NewGuid().ToString(), "Multiplayer Online Battle Arena (MOBA)"),
                new Genre(Guid.NewGuid().ToString(), "Tower Defense (TD)"),
                new Genre(Guid.NewGuid().ToString(), "Turn Based Strategy (TBS)"),
                new Genre(Guid.NewGuid().ToString(), "Turn Based Tactics (TBT)"),
                new Genre(Guid.NewGuid().ToString(), "Trading Card Games (TCG)"),
                new Genre(Guid.NewGuid().ToString(), "Racing"),
                new Genre(Guid.NewGuid().ToString(), "Sports")
            };

            // Seed
            foreach (var genre in genres)
            {
                var storedGenre = await _collection.Find(x => x.Name == genre.Name).FirstOrDefaultAsync();
                if (storedGenre == null)
                {
                    await _collection.InsertOneAsync(genre);
                }
                else
                {
                    var newGenre = new Genre(storedGenre.Id, genre.Name);
                    await _collection.ReplaceOneAsync(x => x.Id == storedGenre.Id, newGenre);
                }
            }
        }
    }
}
