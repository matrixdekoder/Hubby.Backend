using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Buddy.Application.QueryService
{
    public static class BuddyQueryServiceBootstrap
    {
        public static void ConfigureBuddyQueryServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(BuddyQueryServiceBootstrap));
        }
    }
}
