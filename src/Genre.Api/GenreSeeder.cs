using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Api;
using Genre.Application.CommandService.Create;
using Genre.Application.QueryService;
using Genre.Domain;
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
                new CreateGenreCommand(GenreConstants.Platform, "Platform"),
                new CreateGenreCommand(GenreConstants.Shooter, "Shooter"),
                new CreateGenreCommand(GenreConstants.Fighting, "Fighting"),
                new CreateGenreCommand(GenreConstants.BeatEmUp, "Beat 'm up"),
                new CreateGenreCommand(GenreConstants.Stealth, "Stealth"),
                new CreateGenreCommand(GenreConstants.Survival, "Survival"),
                new CreateGenreCommand(GenreConstants.Rhythm, "Rhythm"),
                new CreateGenreCommand(GenreConstants.SurvivalHorror, "Survival horror"),
                new CreateGenreCommand(GenreConstants.ActionRpg, "Action RPG"),
                new CreateGenreCommand(GenreConstants.Mmorpg, "MMORPG"),
                new CreateGenreCommand(GenreConstants.Roguelikes, "Roguelikes"),
                new CreateGenreCommand(GenreConstants.TacticalRpg, "Tactical RPG"),
                new CreateGenreCommand(GenreConstants.SandboxRpg, "Sandbox RPG"),
                new CreateGenreCommand(GenreConstants.Fantasy, "Fantasy"),
                new CreateGenreCommand(GenreConstants.Choices, "Choices"),
                new CreateGenreCommand(GenreConstants.Construction, "Construction and Management simulation"),
                new CreateGenreCommand(GenreConstants.LifeSimulation, "Life simulation"),
                new CreateGenreCommand(GenreConstants.VehicleSimulation, "Vehicle simulation"),
                new CreateGenreCommand(GenreConstants.FourXStrategy, "4X Strategy"),
                new CreateGenreCommand(GenreConstants.Rts, "Real Time Strategy (RTS)"),
                new CreateGenreCommand(GenreConstants.Moba, "Multiplayer Online Battle Arena (MOBA)"),
                new CreateGenreCommand(GenreConstants.Td, "Tower Defense (TD)"),
                new CreateGenreCommand(GenreConstants.Tbs, "Turn Based Strategy (TBS)"),
                new CreateGenreCommand(GenreConstants.Tbt, "Turn Based Tactics (TBT)"),
                new CreateGenreCommand(GenreConstants.Tcg, "Trading Card Games (TCG)"),
                new CreateGenreCommand(GenreConstants.Racing, "Racing"),
                new CreateGenreCommand(GenreConstants.Sports, "Sports"),
            };

            foreach (var command in commands)
            {
                await _mediator.Publish(command);
            }
        }
    }
}
