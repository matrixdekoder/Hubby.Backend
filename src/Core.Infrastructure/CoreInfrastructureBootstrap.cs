using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure
{
    public static class CoreInfrastructureBootstrap
    {
        public static void ConfigureCoreInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IPasswordComputer, PasswordComputer>();
        }
    }
}
