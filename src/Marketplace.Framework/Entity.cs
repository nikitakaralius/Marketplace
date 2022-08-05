namespace Marketplace.Framework;

public abstract class Entity
{
    private readonly List<IEvent> _changes = new();

    protected void Apply(IEvent @event)
    {
        When(eventHappened: @event);
        EnsureValidState();
        _changes.Add(@event);
    }

    public IEnumerable<IEvent> Changes() => _changes;

    public void ClearChanges() => _changes.Clear();

    protected abstract void When(IEvent eventHappened);

    protected abstract void EnsureValidState();
}
