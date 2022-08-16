using EventStore.ClientAPI;

namespace Marketplace.Infrastructure.Store;

internal sealed class EsAggregateStore : IAggregateStore
{
    private const int StreamSlice = 1024;
    private readonly IEventStoreConnection _connection;

    public EsAggregateStore(IEventStoreConnection connection) => _connection = connection;

    public async Task<bool> ExistsAsync<TAggregate, TId>(TId aggregateId)
        where TAggregate : AggregateRoot<TId>
        where TId : notnull
    {
        string stream = StreamName<TAggregate, TId>(aggregateId);
        var response = await _connection.ReadEventAsync(stream, 1, false);
        return response.Status != EventReadStatus.NoStream;
    }

    public async Task SaveAsync<TAggregate, TId>(TAggregate aggregate)
        where TAggregate : AggregateRoot<TId>
        where TId : notnull
    {
        string stream = StreamName<TAggregate, TId>(aggregate);
        await _connection.AppendEventsAsync(stream, aggregate.Version,
                                            aggregate.Changes().ToArray());
    }

    public async Task<TAggregate> LoadAsync<TAggregate, TId>(TId aggregateId)
        where TAggregate : AggregateRoot<TId>
        where TId : notnull
    {
        string stream = StreamName<TAggregate, TId>(aggregateId);
        var aggregate = (TAggregate) Activator.CreateInstance(typeof(TAggregate), true)!;
        var page = await _connection.ReadStreamEventsForwardAsync(stream, 0, StreamSlice, false);

        aggregate.Load(page.Events.Select(e => e.Deserialize()).ToArray());

        return aggregate;
    }

    private static string StreamName<TAggregate, TId>(TId aggregateId) where TId : notnull =>
        $"{typeof(TAggregate).Name}-{aggregateId.ToString()}";

    private static string StreamName<TAggregate, TId>(TAggregate aggregate)
        where TAggregate : AggregateRoot<TId>
        where TId : notnull =>
        $"{typeof(TAggregate).Name}-{aggregate.Id.ToString()}";
}
