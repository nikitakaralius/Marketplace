namespace Marketplace.Framework;

public interface IEntityStore
{
    Task<T?> LoadAsync<T>(string entityId) where T : Entity<T>;

    Task<T> SaveAsync<T>(T entity) where T : Entity<T>;

    Task<bool> ExistsAsync<T>(string entityId) where T : Entity<T>;
}
