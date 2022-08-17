namespace Marketplace.ClassifiedAds.Projections;

public interface IClassifiedAdRepository
{
    Task AddAsync(ClassifiedAdDetails ad);

    Task<ClassifiedAdDetails?> ByIdAsync(Guid id);

    Task SaveChangesAsync();
}
