using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application
{
    public static class CoreApplicationBootstrap
    {
        public static void ConfigureCoreApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CoreApplicationBootstrap));
        }
    }
}
