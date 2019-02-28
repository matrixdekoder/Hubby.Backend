using Buddy.Domain.Factories;
using Core.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Buddy.Application.CommandService
{
    public static class BuddyCommandServiceBootstrap
    {
        public static void ConfigureBuddyCommandServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(BuddyCommandServiceBootstrap));
            services.AddTransient<IAggregateFactory<Domain.Entities.Buddy>, BuddyFactory>();
            services.AddTransient<IAggregateFactory<Domain.Entities.Group>, GroupFactory>();
            services.AddTransient<IAggregateFactory<Domain.Entities.Region>, RegionFactory>();
        }
    }
}
