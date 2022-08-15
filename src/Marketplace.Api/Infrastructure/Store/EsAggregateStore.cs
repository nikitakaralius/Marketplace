using System.Text.Json;
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
        var changes = aggregate.Changes()
                               .Select(e => ConvertToEventData(e))
                               .ToArray();

        if (changes.Length == 0) return;

        string stream = StreamName<TAggregate, TId>(aggregate);
        await _connection.AppendToStreamAsync(stream, aggregate.Version, changes);
    }

    public async Task<TAggregate> LoadAsync<TAggregate, TId>(TId aggregateId)
        where TAggregate : AggregateRoot<TId>
        where TId : notnull
    {
        string stream = StreamName<TAggregate, TId>(aggregateId);
        var aggregate = (TAggregate) Activator.CreateInstance(typeof(TAggregate), true)!;
        var page = await _connection.ReadStreamEventsForwardAsync(stream, 0, StreamSlice, false);

        aggregate.Load(page.Events.Select(e =>
        {
            var meta = JsonSerializer.Deserialize<EventMetadata>(e.Event.Metadata)!;
            var dataType = Type.GetType(meta.ClrType)!;
            var data = JsonSerializer.Deserialize(e.Event.Data, dataType)!;
            return (IEvent) data;
        }).ToArray());

        return aggregate;
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
