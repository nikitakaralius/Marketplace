using Marketplace.Infrastructure.Persistence;

namespace Marketplace.EntityFramework.Concrete;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly MarketplaceDbContext _context;

    public UnitOfWork(MarketplaceDbContext context) => _context = context;

    public async Task CommitAsync() => await _context.SaveChangesAsync();
}
