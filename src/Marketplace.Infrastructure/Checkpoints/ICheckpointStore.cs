namespace Marketplace.Infrastructure.Checkpoints;

public interface ICheckpointStore
{
    Task<Checkpoint> CheckpointByNameAsync(string name);

    Task StoreAsync(Checkpoint checkpoint);
}
