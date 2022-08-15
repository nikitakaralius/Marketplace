using EventStore.ClientAPI;

namespace Marketplace.Infrastructure.Store;

internal sealed class EventStoreService : IHostedService
{
    private readonly IEventStoreConnection _connection;
    private readonly EsSubscription _subscription;

    public EventStoreService(IEventStoreConnection connection, EsSubscription subscription)
    {
        _connection = connection;
        _subscription = subscription;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _connection.ConnectAsync();
        _subscription.Start();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _subscription.Stop();
        _connection.Close();
        return Task.CompletedTask;
    }
}
