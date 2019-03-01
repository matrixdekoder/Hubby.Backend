using Core.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Buddy.Infrastructure.Seeder
{
    public static class BuddyInfrastructureSeederBootstrap
    {
        public static void ConfigureBuddyInfrastructureSeeder(this IServiceCollection services)
        {
            services.AddTransient<ISeeder, RegionSeeder>();
            services.AddTransient<ISeeder, GenreSeeder>();
        }
    }
}
