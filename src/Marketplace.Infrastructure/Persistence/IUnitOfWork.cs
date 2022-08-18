namespace Marketplace.Infrastructure.Persistence;

public interface IUnitOfWork
{
    Task CommitAsync();
}
