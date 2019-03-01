using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Region.Application.QueryService
{
    public static class RegionQueryServiceBootstrap
    {
        public static void ConfigureRegionQueryService(this IServiceCollection services)
        {
            services.AddMediatR(typeof(RegionQueryServiceBootstrap));
        }
    }
}
