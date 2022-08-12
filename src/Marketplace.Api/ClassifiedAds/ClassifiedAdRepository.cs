using Marketplace.Domain.ClassifiedAd;
using Marketplace.Domain.ClassifiedAd.ValueObjects;
using Marketplace.Infrastructure.EntityFramework;

namespace Marketplace.ClassifiedAds;

internal sealed class ClassifiedAdRepository : IClassifiedAdRepository
{
    private readonly MarketplaceDbContext _dbContext;

    public ClassifiedAdRepository(MarketplaceDbContext dbContext) => _dbContext = dbContext;

    public async Task<ClassifiedAd?> LoadAsync(ClassifiedAdId id) =>
        await _dbContext.ClassifiedAds.FindAsync(id.Value);

    public async Task AddAsync(ClassifiedAd entity) =>
        await _dbContext.ClassifiedAds.AddAsync(entity);

    public async Task<bool> ExistsAsync(ClassifiedAdId id) =>
        await _dbContext.ClassifiedAds.FindAsync(id.Value) is not null;
}
