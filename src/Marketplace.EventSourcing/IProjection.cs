namespace Marketplace.EventSourcing;

public interface IProjection
{
    Task ProjectAsync(IEvent @event);
}
