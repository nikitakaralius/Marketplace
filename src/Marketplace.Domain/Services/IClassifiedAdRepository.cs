using Marketplace.Domain.Entities;
using Marketplace.Domain.ValueObjects;

namespace Marketplace.Domain.Services;

public interface IClassifiedAdRepository
{
    Task<ClassifiedAd?> LoadAsync(ClassifiedAdId id);
    
    Task AddAsync(ClassifiedAd entity);

    Task<bool> ExistsAsync(ClassifiedAdId id);
}
