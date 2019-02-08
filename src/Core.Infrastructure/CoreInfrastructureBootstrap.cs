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
                cfg.Expiration = int.Parse(configuration.GetSection("Token:Expiration").Value);
            });

            services.AddTransient<IPasswordComputer, PasswordComputer>();
            services.AddTransient<ITokenHandler, TokenHandler>();
        }
    }
}