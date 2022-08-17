using Marketplace.ClassifiedAds.Projections;

namespace Marketplace.Infrastructure.Persistence;

public interface IClassifiedAdRepository
{
    Task AddAsync(ClassifiedAdDetails ad);

    Task<ClassifiedAdDetails?> ByIdAsync(Guid id);
}
