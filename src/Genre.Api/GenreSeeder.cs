using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Api;
using Genre.Application.CommandService.Create;
using Genre.Application.QueryService;
using Library.Mongo;
using MediatR;
using MongoDB.Driver;

namespace Genre.Api
{
    public class GenreSeeder : ISeeder
    {
        private readonly IMediator _mediator;
        private readonly IMongoCollection<GenreReadModel> _collection;

        public GenreSeeder(IMongoContext context, IMediator mediator)
        {
            _mediator = mediator;
            _collection = context.GetCollection<GenreReadModel>();
        }

        public async Task Seed()
        {
            await _collection.DeleteManyAsync(FilterDefinition<GenreReadModel>.Empty);

            var commands = new List<CreateGenreCommand>
            {
                new CreateGenreCommand("Platform"),
                new CreateGenreCommand("Shooter"),
                new CreateGenreCommand("Fighting"),
                new CreateGenreCommand("Beat 'm up"),
                new CreateGenreCommand("Stealth"),
                new CreateGenreCommand("Survival"),
                new CreateGenreCommand("Rhythm"),
                new CreateGenreCommand("Survival horror"),
                new CreateGenreCommand("Action RPG"),
                new CreateGenreCommand("MMORPG"),
                new CreateGenreCommand("Roguelikes"),
                new CreateGenreCommand("Tactical RPG"),
                new CreateGenreCommand("Sandbox RPG"),
                new CreateGenreCommand("Fantasy"),
                new CreateGenreCommand("Choices"),
                new CreateGenreCommand("Construction and Management simulation"),
                new CreateGenreCommand("Life simulation"),
                new CreateGenreCommand("Vehicle simulation"),
                new CreateGenreCommand("4X Strategy"),
                new CreateGenreCommand("Real Time Strategy (RTS)"),
                new CreateGenreCommand("Multiplayer Online Battle Arena (MOBA)"),
                new CreateGenreCommand("Tower Defense (TD)"),
                new CreateGenreCommand("Turn Based Strategy (TBS)"),
                new CreateGenreCommand("Turn Based Tactics (TBT)"),
                new CreateGenreCommand("Trading Card Games (TCG)"),
                new CreateGenreCommand("Racing"),
                new CreateGenreCommand("Sports"),
            };

            foreach (var command in commands)
            {
                await _mediator.Publish(command);
            }
        }
    }
}
