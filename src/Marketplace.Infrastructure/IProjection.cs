namespace Marketplace.Infrastructure;

public interface IProjection
{
    Task ProjectAsync(IEvent @event);
}
