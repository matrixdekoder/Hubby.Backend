using Account.Application.CommandService;
using Account.Application.QueryService;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Api
{
    public static class AccountBootstrap
    {
        public static void ConfigureAccount(this IServiceCollection services)
        {
            services.AddMvc().AddApplicationPart(typeof(AccountBootstrap).Assembly);
            services.ConfigureAccountCommandServices();
            services.ConfigureAccountQueryServices();
        }
    }
}
