using Core.Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure
{
    public static class CoreInfrastructureBootstrap
    {
        public static void ConfigureCoreInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenConfiguration>(cfg =>
            {
                cfg.Secret = configuration.GetSection("Token:Secret").Value;
                cfg.AccessExpirationSeconds = int.Parse(configuration.GetSection("Token:AccessExpirationSeconds").Value);
                cfg.RefreshExpirationSeconds = int.Parse(configuration.GetSection("Token:RefreshExpirationSeconds").Value);
            });

            services.AddTransient<IPasswordComputer, PasswordComputer>();
            services.AddTransient<ITokenHandler, TokenHandler>();
        }
    }
}