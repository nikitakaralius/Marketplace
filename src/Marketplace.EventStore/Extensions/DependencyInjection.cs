using Marketplace.ClassifiedAds.Projections;
using Marketplace.EventStore.Appliers;
using Marketplace.Infrastructure.Checkpoints;
using Marketplace.Users.Projections;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.EventStore.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddEventStoreModule(this IServiceCollection services,
                                                         Action<EventStoreOptions> configure)
    {
        EventStoreOptions options = new();
        configure(options);

        var esConnection = EventStoreConnection.Create(
            options.ConnectionString,
            ConnectionSettings.Create().KeepReconnecting(),
            options.ConnectionName);

        services.AddSingleton(esConnection);
        services.AddSingleton<IAggregateStore, AggregateStore>();

        services.AddSingleton(s =>
        {
            var factory = s.GetRequiredService<IServiceScopeFactory>();
            var adRepository = new AdRepositoryApplier(factory);
            var userRepository = new UserRepositoryApplier(factory);
            var store = new CheckpointStoreApplier(factory);

            IProjection[] projections =
            {
                new ClassifiedAdDetailsProjection(adRepository),
                new UserDetailsProjection(userRepository)
            };

            return new ProjectionDispatcher(esConnection, store, projections);
        });

        services.AddHostedService<EventStoreService>();

        return services;
    }
}

public class EventStoreOptions
{
    public string ConnectionString { get; set; } = "";

    public string ConnectionName { get; set; } = "";
}
