namespace Marketplace.EntityFramework.Concrete;

internal sealed class ClassifiedAdRepository : IClassifiedAdRepository
{
    private readonly MarketplaceDbContext _context;

    public ClassifiedAdRepository(MarketplaceDbContext context) =>
        _context = context;

    public async Task AddAsync(ClassifiedAdDetails ad) =>
        await _context.ClassifiedAds.AddAsync(ad);

    public async Task<ClassifiedAdDetails?> ByIdAsync(Guid id) =>
        await _context.ClassifiedAds.FindAsync(id);
}
