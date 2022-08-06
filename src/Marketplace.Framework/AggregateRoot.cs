namespace Marketplace.Framework;

public abstract class AggregateRoot : IInternalEventHandler
{
    private readonly List<IEvent> _changes = new();

    public void ClearChanges() => _changes.Clear();

    public IEnumerable<IEvent> Changes() => _changes;

    protected abstract void When(IEvent eventHappened);

    protected abstract void EnsureValidState();

    protected void Apply(IEvent @event)
    {
        When(eventHappened: @event);
        EnsureValidState();
        _changes.Add(@event);
    }

    protected void ApplyToEntity(IInternalEventHandler entity, IEvent @event) =>
        entity?.Handle(@event);

    void IInternalEventHandler.Handle(IEvent @event) =>
        When(eventHappened: @event);
}
