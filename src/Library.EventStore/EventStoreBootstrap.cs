using Core.Domain;
using Library.EventStore.Configurations;
using Library.EventStore.Handlers;
using Library.EventStore.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.EventStore
{
    public static class EventStoreBootstrap
    {
        public static void ConfigureEventStore(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EventStoreConfiguration>(cfg =>
            {
                cfg.Username = configuration.GetSection("EventStoreOptions:Username").Value;
                cfg.Password = configuration.GetSection("EventStoreOptions:Password").Value;
                cfg.Address = configuration.GetSection("EventStoreOptions:Address").Value;
                cfg.Port = configuration.GetSection("EventStoreOptions:Port").Value;
            });
            
            services.AddSingleton<IEventStoreContext, EventStoreContext>();
            services.AddTransient<IRepository, EventStoreRepository>();
            services.AddTransient<IQueryStreamHandler, QueryStreamHandler>();
            services.AddTransient<IEventStoreListener, QueryStreamListener>();
            services.AddTransient<ICommandStreamHandler, CommandStreamHandler>();
            services.AddTransient<IEventStoreListener, CommandStreamListener>();
        }
    }
}
