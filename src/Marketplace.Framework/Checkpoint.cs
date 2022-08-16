namespace Marketplace.Framework;

public sealed class Checkpoint
{
    public string Name { get; init; } = "";

    public long CommitPosition { get; init; }

    public long PreparePosition { get; init; }
}
