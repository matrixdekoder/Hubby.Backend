using System.Reflection;
using Account.Application.CommandService;
using Account.Application.QueryService;
using Core.Infrastructure;
using Library.EventStore;
using Library.Mongo;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Host.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Framework services
            services.AddMvc();
            services.AddCors(options => options.AddPolicy("AllowAll", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

            // Library services
            services.AddMediatR(typeof(Startup));
            services.ConfigureEventStore(Configuration);
            services.ConfigureMongoDb(Configuration);

            // Core
            services.ConfigureCoreInfrastructure();

            // Application services
            services.ConfigureAccountCommandServices();
            services.ConfigureAccountQueryServices();
        }
        
        public void Configure(IApplicationBuilder app)
        {
            CatchUpEvents(app);
            app.UseMvc();
            app.UseCors("AllowAll");
        }

        private void CatchUpEvents(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var eventStoreListener = scope.ServiceProvider.GetService<IEventStoreListener>();
            eventStoreListener.Listen();
        }
    }
}
