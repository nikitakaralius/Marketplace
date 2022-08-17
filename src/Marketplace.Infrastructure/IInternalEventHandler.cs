namespace Marketplace.Infrastructure;

public interface IInternalEventHandler
{
    void Handle(IEvent @event);
}
