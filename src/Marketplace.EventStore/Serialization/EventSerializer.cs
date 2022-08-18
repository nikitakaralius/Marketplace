using System.Text.Json;

namespace Marketplace.EventStore.Serialization;

internal static class EventSerializer
{
    public static IEvent Deserialize(this ResolvedEvent e)
    {
        var meta = JsonSerializer.Deserialize<EventMetadata>(e.Event.Metadata)!;
        var dataType = Type.GetType(meta.ClrType)!;
        var data = JsonSerializer.Deserialize(e.Event.Data, dataType)!;
        return (IEvent) data;
    }

    public static EventData Serialize(this IEvent e) =>
        new(eventId: Guid.NewGuid(),
            type: e.GetType().Name,
            isJson: true,
            data: Serialize((object) e),
            metadata: Serialize(new EventMetadata
            {
                ClrType = e.GetType().AssemblyQualifiedName!
            }));

    private static byte[] Serialize(object data) =>
        JsonSerializer.SerializeToUtf8Bytes(data);
}
