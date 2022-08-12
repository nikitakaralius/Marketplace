namespace Marketplace.Framework;

internal interface IUnitOfWork
{
    Task CommitAsync();
}
