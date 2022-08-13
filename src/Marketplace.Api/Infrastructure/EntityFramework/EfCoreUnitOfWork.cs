namespace Marketplace.Infrastructure.EntityFramework;

internal sealed class EfCoreUnitOfWork : IUnitOfWork
{
    private readonly MarketplaceDbContext _dbContext;

    public EfCoreUnitOfWork(MarketplaceDbContext dbContext) => _dbContext = dbContext;

    public async Task CommitAsync() => await _dbContext.SaveChangesAsync();
}
