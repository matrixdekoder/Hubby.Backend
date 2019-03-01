using Core.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Region.Domain;

namespace Region.Application.CommandService
{
    public static class RegionCommandServiceBootstrap
    {
        public static void ConfigureRegionCommandService(this IServiceCollection services)
        {
            services.AddMediatR(typeof(RegionCommandServiceBootstrap));
            services.AddTransient<IAggregateFactory<Domain.Region>, RegionFactory>();
        }
    }
}
