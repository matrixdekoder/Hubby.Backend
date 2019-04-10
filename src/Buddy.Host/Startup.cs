using System;
using Account.Api;
using Buddy.Api;
using Core.Api;
using Core.Application;
using Core.Infrastructure;
using Genre.Api;
using Library.EventStore;
using Library.EventStore.Handlers;
using Library.Mongo;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Region.Api;

namespace Buddy.Host
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
            // Core
            services.ConfigureCoreApiServices(Configuration);
            services.ConfigureCoreInfrastructure(Configuration);
            services.ConfigureCoreApplication();

            // Library services
            services.AddMediatR(typeof(Startup));
            services.ConfigureEventStore(Configuration);
            services.ConfigureMongoDb(Configuration);

            // Application services
            services.ConfigureGenre();
            services.ConfigureBuddy();
            services.ConfigureAccount();
            services.ConfigureRegion();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.ConfigureCoreApi();
            StartEventStore(app);
            SeedDatabase(app);
        }

        private void SeedDatabase(IApplicationBuilder app)
        {
            var seed = Convert.ToBoolean(Configuration.GetSection("Seed").Value);
            if (!seed) return;

            var scope = app.ApplicationServices.CreateScope();
            var seeders = scope.ServiceProvider.GetServices<ISeeder>();
            foreach (var seeder in seeders)
            {
                seeder.Seed();
            }
        }

        private static void StartEventStore(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var eventStoreListener = scope.ServiceProvider.GetService<IEventStoreListener>();
            eventStoreListener.Listen();
        }
    }
}