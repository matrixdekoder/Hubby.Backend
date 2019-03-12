using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Region.Application.CommandService
{
    public static class RegionCommandServiceBootstrap
    {
        public static void ConfigureRegionCommandService(this IServiceCollection services)
        {
            services.AddMediatR(typeof(RegionCommandServiceBootstrap));
        }
    }
}
