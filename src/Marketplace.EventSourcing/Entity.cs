namespace Marketplace.EventSourcing;

public abstract class Entity : IInternalEventHandler
{
    private readonly Action<IEvent> _applier = null!;

    protected Entity(Action<IEvent> applier) =>
        _applier = applier;

    protected Entity() { }

    protected abstract void When(IEvent eventHappened);

    protected void Apply(IEvent @event)
    {
        When(eventHappened: @event);
        _applier(@event);
    }

    void IInternalEventHandler.Handle(IEvent @event) => When(eventHappened: @event);
}
