using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Api;
using Library.Mongo.Persistence;
using MediatR;
using MongoDB.Driver;
using Region.Application.CommandService.Create;
using Region.Application.QueryService;
using Region.Domain;

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
            var commands = new List<CreateRegionCommand>
            {
                new CreateRegionCommand(RegionConstants.Europe, "Europe"),
                new CreateRegionCommand(RegionConstants.Asia, "Asia"),
                new CreateRegionCommand(RegionConstants.Australia, "Australia"),
                new CreateRegionCommand(RegionConstants.NorthAmerica, "North America"),
                new CreateRegionCommand(RegionConstants.SouthAmerica, "South America")
            };

            foreach (var command in commands)
            {
                await _mediator.Publish(command);
            }
        }
    }
}
