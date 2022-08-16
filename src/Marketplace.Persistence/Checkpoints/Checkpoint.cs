using EventStore.ClientAPI;

namespace Marketplace.Persistence.Checkpoints;

public sealed class Checkpoint
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    private long CommitPosition { get; init; }

    private long PreparePosition { get; init; }

    public Position Position => new(CommitPosition, PreparePosition);
}
