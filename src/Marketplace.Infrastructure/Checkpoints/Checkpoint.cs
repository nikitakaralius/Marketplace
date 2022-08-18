namespace Marketplace.Infrastructure.Checkpoints;

public sealed class Checkpoint
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public long CommitPosition { get; set; }

    public long PreparePosition { get; set; }
}
