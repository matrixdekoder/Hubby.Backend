using System.Collections.Generic;
using System.Threading.Tasks;
using Buddy.Domain;
using Core.Infrastructure;
using Library.Mongo;

namespace Buddy.Infrastructure.Seed
{
    public class RegionSeeder: Seeder<Region>
    {

        public RegionSeeder(IMongoContext mongoContext): base(mongoContext)
        {
        }

        public override async Task Seed()
        {
            var regions = new List<Region>
            {
                new Region("Europe"),
                new Region("Asia"),
                new Region("Africa"),
                new Region("Australia"),
                new Region("North America"),
                new Region("South America")
            };

            foreach (var region in regions)
            {
                await Store(region, x => x.Name == region.Name);
            }
        }
    }
}
