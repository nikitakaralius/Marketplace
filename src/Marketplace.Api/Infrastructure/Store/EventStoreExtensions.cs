using EventStore.ClientAPI;

namespace Marketplace.Infrastructure.Store;

internal static class EventStoreExtensions
{
    public static Task AppendEventsAsync(
        this IEventStoreConnection connection,
        string streamName, long version,
        params IEvent[] events)
    {
        if (events.Length == 0) return Task.CompletedTask;
        var changes = events.Select(e => e.Serialize()).ToArray();
        return connection.AppendToStreamAsync(streamName, version, changes);
    }
}
