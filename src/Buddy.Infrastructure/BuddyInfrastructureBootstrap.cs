using Buddy.Domain.Services;
using Buddy.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Buddy.Infrastructure
{
    public static class BuddyInfrastructureBootstrap
    {
        public static void ConfigureBuddyInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IMatchService, MatchService>();
        }
    }
}
