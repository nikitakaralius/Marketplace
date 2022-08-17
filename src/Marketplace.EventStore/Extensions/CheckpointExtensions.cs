using Marketplace.Infrastructure.Checkpoints;

namespace Marketplace.EventStore.Extensions;

internal static class CheckpointExtensions
{
    public static Position ToPosition(this Checkpoint? checkpoint) =>
        checkpoint is null
            ? Position.Start
            : new(checkpoint.CommitPosition, checkpoint.PreparePosition);

    public static Checkpoint ToCheckpoint(this Position position, string name) =>
        new()
        {
            Name = name,
            CommitPosition = position.CommitPosition,
            PreparePosition = position.PreparePosition
        };
}
