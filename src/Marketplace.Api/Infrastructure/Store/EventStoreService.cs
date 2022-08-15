using EventStore.ClientAPI;

namespace Marketplace.Infrastructure.Store;

internal sealed class EventStoreService : IHostedService
{
    private readonly IEventStoreConnection _connection;

    public EventStoreService(IEventStoreConnection connection) =>
        _connection = connection;

    public Task StartAsync(CancellationToken cancellationToken) =>
        _connection.ConnectAsync();

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _connection.Close();
        return Task.CompletedTask;
    }
}
