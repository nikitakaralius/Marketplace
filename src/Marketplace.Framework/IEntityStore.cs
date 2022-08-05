namespace Marketplace.Framework;

public interface IEntityStore
{
    Task<T?> LoadAsync<T>(string entityId) where T : Entity;

    Task<T> SaveAsync<T>(T entity) where T : Entity;

    Task<bool> ExistsAsync<T>(string entityId) where T : Entity;
}
