namespace Marketplace.Framework;

public abstract class Entity : IInternalEventHandler
{
    private readonly Action<IEvent> _applier;

    protected Entity(Action<IEvent> applier) =>
        _applier = applier;

    protected abstract void When(IEvent eventHappened);

    protected void Apply(IEvent @event)
    {
        When(eventHappened: @event);
        _applier(@event);
    }

    void IInternalEventHandler.Handle(IEvent @event) => When(eventHappened: @event);
}
