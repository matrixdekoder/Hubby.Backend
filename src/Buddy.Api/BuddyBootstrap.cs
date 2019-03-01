using Buddy.Application.CommandService;
using Buddy.Application.QueryService;
using Buddy.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Buddy.Api
{
    public static class BuddyBootstrap
    {
        public static void ConfigureBuddy(this IServiceCollection services)
        {
            services.AddMvc().AddApplicationPart(typeof(BuddyBootstrap).Assembly);
            services.ConfigureBuddyCommandServices();
            services.ConfigureBuddyQueryServices();
            services.ConfigureBuddyInfrastructure();
        }
    }
}
