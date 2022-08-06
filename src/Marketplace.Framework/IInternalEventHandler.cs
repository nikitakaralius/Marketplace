namespace Marketplace.Framework;

public interface IInternalEventHandler
{
    void Handle(IEvent @event);
}
