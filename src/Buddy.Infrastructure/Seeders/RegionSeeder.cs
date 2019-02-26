using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Buddy.Domain.Entities;
using Core.Infrastructure;
using Library.Mongo;
using MongoDB.Driver;

namespace Buddy.Infrastructure.Seeders
{
    public class RegionSeeder : ISeeder
    {
        private readonly IMongoCollection<Region> _collection;

        public RegionSeeder(IMongoContext context)
        {
            _collection = context.GetCollection<Region>();
        }

        public async Task Seed()
        {
            var regions = new List<Region>
            {
                new Region(Guid.NewGuid().ToString(), "Europe"),
                new Region(Guid.NewGuid().ToString(), "Asia"),
                new Region(Guid.NewGuid().ToString(), "Africa"),
                new Region(Guid.NewGuid().ToString(), "Australia"),
                new Region(Guid.NewGuid().ToString(), "North America"),
                new Region(Guid.NewGuid().ToString(), "South America")
            };

            // Seed
            foreach (var region in regions)
            {
                var storedRegion = await _collection.Find(x => x.Name == region.Name).FirstOrDefaultAsync();
                if (storedRegion == null)
                {
                    await _collection.InsertOneAsync(region);
                }
                else
                {
                    var newRegion = new Region(storedRegion.Id, region.Name);
                    await _collection.ReplaceOneAsync(x => x.Id == storedRegion.Id, newRegion);
                }
            }
        }
    }
}
