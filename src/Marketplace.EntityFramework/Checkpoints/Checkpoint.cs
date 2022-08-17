namespace Marketplace.EntityFramework.Checkpoints;

public sealed class Checkpoint
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    private long CommitPosition { get; init; }

    private long PreparePosition { get; init; }
}