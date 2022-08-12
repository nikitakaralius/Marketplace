using Marketplace.Framework;

namespace Marketplace.Infrastructure.EntityFramework;

internal sealed class EfCoreUnitOfWork : IUnitOfWork
{
    private readonly ClassifiedAdDbContext _dbContext;

    public EfCoreUnitOfWork(ClassifiedAdDbContext dbContext) => _dbContext = dbContext;

    public async Task CommitAsync() => await _dbContext.SaveChangesAsync();
}
