using Marketplace.Domain.ClassifiedAd;
using Marketplace.Domain.ClassifiedAd.ValueObjects;

namespace Marketplace.Infrastructure.EntityFramework;

internal sealed class ClassifiedAdRepository : IClassifiedAdRepository
{
    private readonly ClassifiedAdDbContext _dbContext;

    public ClassifiedAdRepository(ClassifiedAdDbContext dbContext) => _dbContext = dbContext;

    public async Task<ClassifiedAd?> LoadAsync(ClassifiedAdId id) =>
        await _dbContext.ClassifiedAds.FindAsync(id.Value);

    public async Task AddAsync(ClassifiedAd entity) =>
        await _dbContext.ClassifiedAds.AddAsync(entity);

    public async Task<bool> ExistsAsync(ClassifiedAdId id) =>
        await _dbContext.ClassifiedAds.FindAsync(id.Value) is not null;
}
