using Microsoft.Extensions.Hosting;

namespace Marketplace.EventStore.Concrete;

internal sealed class EventStoreService : IHostedService
{
    private readonly IEventStoreConnection _connection;
    private readonly ProjectionDispatcher _dispatcher;

    public EventStoreService(IEventStoreConnection connection, ProjectionDispatcher dispatcher)
    {
        _connection = connection;
        _dispatcher = dispatcher;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _connection.ConnectAsync();
        await _dispatcher.StartAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _dispatcher.Stop();
        _connection.Close();
        return Task.CompletedTask;
    }
}
