using EventStore.ClientAPI;

namespace Marketplace.Persistence.Checkpoints;

public sealed class Checkpoint
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public Position Position { get; set; }
}
