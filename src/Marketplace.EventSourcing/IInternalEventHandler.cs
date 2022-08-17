namespace Marketplace.EventSourcing;

public interface IInternalEventHandler
{
    void Handle(IEvent @event);
}
