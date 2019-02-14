using Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Buddy.Infrastructure.Seed
{
    public static class BuddyInfrastructureSeedBootstrap
    {
        public static void ConfigureBuddyInfrastructureSeed(this IServiceCollection services)
        {
            services.AddTransient<ISeeder, RegionSeeder>();
        }
    }
}
