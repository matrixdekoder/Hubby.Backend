using Buddy.Domain.Services;
using Buddy.Infrastructure.Seeders;
using Buddy.Infrastructure.Services;
using Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Buddy.Infrastructure
{
    public static class BuddyInfrastructureBootstrap
    {
        public static void ConfigureBuddyInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ISeeder, RegionSeeder>();
            services.AddTransient<ISeeder, GenreSeeder>();
            services.AddTransient<IMatchService, MatchService>();
        }
    }
}
