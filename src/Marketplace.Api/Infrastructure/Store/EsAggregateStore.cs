using System.Text.Json;
using EventStore.ClientAPI;

namespace Marketplace.Infrastructure.Store;

internal sealed class EsAggregateStore : IAggregateStore
{
    private readonly IEventStoreConnection _connection;

    public EsAggregateStore(IEventStoreConnection connection) => _connection = connection;

    public Task<bool> ExistsAsync<TAggregate, TId>(TId aggregateId)
        where TAggregate : AggregateRoot<TId>
        where TId : notnull
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync<TAggregate, TId>(TAggregate aggregate)
        where TAggregate : AggregateRoot<TId>
        where TId : notnull
    {
        var changes = aggregate.Changes()
                               .Select(e => ConvertToEventData(e))
                               .ToArray();

        if (changes.Length == 0) return;

        string stream = StreamName<TAggregate, TId>(aggregate);
        await _connection.AppendToStreamAsync(stream, aggregate.Version, changes);
    }

    public Task<TAggregate> LoadAsync<TAggregate, TId>(TId aggregateId)
        where TAggregate : AggregateRoot<TId>
        where TId : notnull
    {
        throw new NotImplementedException();
    }

    private static string StreamName<TAggregate, TId>(TId aggregateId) where TId : notnull =>
        $"{typeof(TAggregate).Name}-{aggregateId.ToString()}";

    private static string StreamName<TAggregate, TId>(TAggregate aggregate)
        where TAggregate : AggregateRoot<TId>
        where TId : notnull =>
        $"{typeof(TAggregate).Name}-{aggregate.Id.ToString()}";

    private static byte[] Serialize(object data) =>
        JsonSerializer.SerializeToUtf8Bytes(data);

    private static EventData ConvertToEventData(IEvent e) =>
        new(eventId: Guid.NewGuid(),
            type: e.GetType().Name,
            isJson: true,
            data: Serialize(e),
            metadata: Serialize(new EventMetadata
            {
                ClrType = e.GetType().AssemblyQualifiedName!
            }));

    private class EventMetadata
    {
        public string ClrType { get; init; } = "";
    }
}
