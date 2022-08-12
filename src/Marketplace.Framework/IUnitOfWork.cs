namespace Marketplace.Framework;

public interface IUnitOfWork
{
    Task CommitAsync();
}
