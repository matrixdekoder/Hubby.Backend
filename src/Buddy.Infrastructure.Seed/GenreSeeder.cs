using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buddy.Domain;
using Core.Infrastructure;
using Library.Mongo;
using MongoDB.Driver;

namespace Buddy.Infrastructure.Seed
{
    public class GenreSeeder: ISeeder
    {
        private readonly IMongoCollection<Genre> _collection;

        public GenreSeeder(IMongoContext context)
        {
            _collection = context.GetCollection<Genre>();
        }

        public async Task Seed()
        {
            // Action
            var platform = new Genre(Guid.NewGuid().ToString(), "Platform", new List<Genre>());
            var shooter = new Genre(Guid.NewGuid().ToString(), "Shooter", new List<Genre>());
            var fighting = new Genre(Guid.NewGuid().ToString(), "Fighting", new List<Genre>());
            var beatEmUp = new Genre(Guid.NewGuid().ToString(), "Beat 'm up", new List<Genre>());
            var stealth = new Genre(Guid.NewGuid().ToString(), "Stealth", new List<Genre>());
            var survival = new Genre(Guid.NewGuid().ToString(), "Survival", new List<Genre>());
            var rhythm = new Genre(Guid.NewGuid().ToString(), "Rhythm", new List<Genre>());
            var survivalHorror = new Genre(Guid.NewGuid().ToString(), "Survival horror", new List<Genre>());

            var action = new Genre(Guid.NewGuid().ToString(), "Action", new List<Genre>
            {
                platform, shooter, fighting, beatEmUp, stealth, survival, rhythm, survivalHorror
            });

            // Adventure
            var textAdventures = new Genre(Guid.NewGuid().ToString(), "Text adventures", new List<Genre>());
            var graphicAdventures = new Genre(Guid.NewGuid().ToString(), "Graphic adventures", new List<Genre>());
            var visualNovels = new Genre(Guid.NewGuid().ToString(), "Visual novels", new List<Genre>());

            var adventure = new Genre(Guid.NewGuid().ToString(), "Adventure", new List<Genre>
            {
                textAdventures, graphicAdventures, visualNovels
            });

            // Role-playing
            var actionRpg = new Genre(Guid.NewGuid().ToString(), "Action RPG", new List<Genre>());
            var mmoRpg = new Genre(Guid.NewGuid().ToString(), "MMORPG", new List<Genre>());
            var rogueLikes = new Genre(Guid.NewGuid().ToString(), "Roguelikes", new List<Genre>());
            var tacticalRpg = new Genre(Guid.NewGuid().ToString(), "Tactical RPG", new List<Genre>());
            var sandboxRpg = new Genre(Guid.NewGuid().ToString(), "Sandbox RPG", new List<Genre>());
            var fantasy = new Genre(Guid.NewGuid().ToString(), "Fantasy", new List<Genre>());
            var choices = new Genre(Guid.NewGuid().ToString(), "Choices", new List<Genre>());

            var rolePlaying = new Genre(Guid.NewGuid().ToString(), "Role-playing", new List<Genre>
            {
                actionRpg, mmoRpg, rogueLikes, tacticalRpg, sandboxRpg, fantasy, choices
            });

            // Simulation
            var constructionManagement = new Genre(Guid.NewGuid().ToString(), "Construction and Management simulation", new List<Genre>());
            var lifeSimulation = new Genre(Guid.NewGuid().ToString(), "Life simulation", new List<Genre>());
            var vehicleSimulation = new Genre(Guid.NewGuid().ToString(), "Vehicle simulation", new List<Genre>());

            var simulation = new Genre(Guid.NewGuid().ToString(), "Simulation", new List<Genre>
            {
                constructionManagement, lifeSimulation, vehicleSimulation
            });

            // Strategy
            var fourX = new Genre(Guid.NewGuid().ToString(), "4X Strategy", new List<Genre>());
            var rts = new Genre(Guid.NewGuid().ToString(), "Real Time Strategy (RTS)", new List<Genre>());
            var moba = new Genre(Guid.NewGuid().ToString(), "Multiplayer Online Battle Arena (MOBA)", new List<Genre>());
            var tower = new Genre(Guid.NewGuid().ToString(), "Tower Defense (TD)", new List<Genre>());
            var tbs = new Genre(Guid.NewGuid().ToString(), "Turn Based Strategy (TBS)", new List<Genre>());
            var tbt = new Genre(Guid.NewGuid().ToString(), "Turn Based Tactics (TBT)", new List<Genre>());
            var card = new Genre(Guid.NewGuid().ToString(), "Card game", new List<Genre>());

            var strategy = new Genre(Guid.NewGuid().ToString(), "Strategy", new List<Genre>
            {
                fourX, rts, moba, tower, tbs, tbt, card
            });

            // Sport
            var racing = new Genre(Guid.NewGuid().ToString(), "Racing", new List<Genre>());
            var sports = new Genre(Guid.NewGuid().ToString(), "Sports game", new List<Genre>());

            var sport = new Genre(Guid.NewGuid().ToString(), "Sport", new List<Genre>
            {
                racing, sports
            });

            
            var genres = new List<Genre>()
            {
                action, adventure, rolePlaying, simulation, strategy, sport
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
                    var newGenre = new Genre(storedGenre.Id, genre.Name, genre.SubGenres.ToList());
                    await _collection.ReplaceOneAsync(x => x.Id == storedGenre.Id, newGenre);
                }
            }
        }
    }
}
