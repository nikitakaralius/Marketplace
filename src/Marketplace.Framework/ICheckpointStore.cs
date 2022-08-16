namespace Marketplace.Framework;

public interface ICheckpointStore
{
    Task<Checkpoint> GetCheckpointAsync(string name);

    Task SaveAsync(Checkpoint checkpoint);
}
