using EventStore.ClientAPI;

namespace Marketplace.Infrastructure.Store;

internal sealed class EsStoreHostedService : IHostedService
{
    private readonly IEventStoreConnection _connection;

    public EsStoreHostedService(IEventStoreConnection connection) =>
        _connection = connection;

    public Task StartAsync(CancellationToken cancellationToken) =>
        _connection.ConnectAsync();

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _connection.Close();
        return Task.CompletedTask;
    }
}
