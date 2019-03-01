using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Api;
using Library.Mongo;
using MediatR;
using MongoDB.Driver;
using Region.Application.CommandService.Create;
using Region.Application.QueryService;

namespace Region.Api
{
    public class RegionSeeder : ISeeder
    {
        private readonly IMongoCollection<RegionReadModel> _collection;
        private readonly IMediator _mediator;

        public RegionSeeder(IMongoContext context, IMediator mediator)
        {
            _collection = context.GetCollection<RegionReadModel>();
            _mediator = mediator;
        }

        public async Task Seed()
        {
            await _collection.DeleteManyAsync(FilterDefinition<RegionReadModel>.Empty);

            var commands = new List<CreateRegionCommand>
            {
                new CreateRegionCommand("Europe"),
                new CreateRegionCommand("Asia"),
                new CreateRegionCommand("Australia"),
                new CreateRegionCommand("North America"),
                new CreateRegionCommand("South America")
            };

            foreach (var command in commands)
            {
                await _mediator.Publish(command);
            }
        }
    }
}
