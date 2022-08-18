namespace Marketplace.EntityFramework.Concrete;

internal sealed class CheckpointStore : ICheckpointStore
{
    private readonly MarketplaceDbContext _context;
    private readonly IUnitOfWork _uow;

    public CheckpointStore(MarketplaceDbContext context, IUnitOfWork uow)
    {
        _context = context;
        _uow = uow;
    }

    public async Task<Checkpoint?> CheckpointByNameAsync(string name) =>
        await _context.Checkpoints.FirstOrDefaultAsync(x => x.Name == name);

    public async Task StoreAsync(Checkpoint checkpoint)
    {
        var entity = await CheckpointByNameAsync(checkpoint.Name);

        if (entity is null)
        {
            await _context.Checkpoints.AddAsync(checkpoint);
        }
        else
        {
            entity.CommitPosition = checkpoint.CommitPosition;
            entity.PreparePosition = checkpoint.PreparePosition;
        }

        await _uow.CommitAsync();
    }
}
