using Marketplace.Domain.ClassifiedAd.ValueObjects;

namespace Marketplace.Domain.ClassifiedAd;

public interface IClassifiedAdRepository
{
    Task<ClassifiedAd?> LoadAsync(ClassifiedAdId id);

    Task AddAsync(ClassifiedAd entity);

    Task<bool> ExistsAsync(ClassifiedAdId id);
}
