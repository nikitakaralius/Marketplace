namespace Marketplace.EFCore.Entities;

public sealed class Checkpoint
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public long CommitPosition { get; init; }

    public long PreparePosition { get; init; }
}
