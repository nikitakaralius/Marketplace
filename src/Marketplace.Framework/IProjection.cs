namespace Marketplace.Framework;

public interface IProjection
{
    Task ProjectAsync(IEvent @event);
}
