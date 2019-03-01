using Microsoft.Extensions.DependencyInjection;
using Region.Application.CommandService;
using Region.Application.QueryService;

namespace Region.Api
{
    public static class RegionBootstrap
    {
        public static void ConfigureRegion(this IServiceCollection services)
        {
            services.AddMvc().AddApplicationPart(typeof(RegionBootstrap).Assembly);
            services.ConfigureRegionCommandService();
            services.ConfigureRegionQueryService();
        }
    }
}
