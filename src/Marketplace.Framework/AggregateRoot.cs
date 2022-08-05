namespace Marketplace.Framework;

public abstract record AggregateRoot<TId>
{
    private readonly List<IEvent> _changes = new();

    public TId Id { get; protected set; }

    public void ClearChanges() => _changes.Clear();

    public IEnumerable<IEvent> Changes() => _changes;

    protected abstract void When(IEvent @event);

    protected abstract void EnsureValidState();

    protected void Apply(IEvent @event)
    {
        When(@event);
        EnsureValidState();
        _changes.Add(@event);
    }
}
