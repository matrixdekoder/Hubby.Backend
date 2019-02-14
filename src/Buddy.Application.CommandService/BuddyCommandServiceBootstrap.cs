using Buddy.Domain;
using Core.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Buddy.Application.CommandService
{
    public static class BuddyCommandServiceBootstrap
    {
        public static void ConfigureBuddyCommandServices(this IServiceCollection services)
        {
            services.AddTransient<IAggregateFactory<Domain.Buddy>, BuddyFactory>();
        }
    }
}
