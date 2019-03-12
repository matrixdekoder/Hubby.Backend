using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Buddy.Application.CommandService
{
    public static class BuddyCommandServiceBootstrap
    {
        public static void ConfigureBuddyCommandServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(BuddyCommandServiceBootstrap));
        }
    }
}
