namespace Marketplace.Infrastructure;

public abstract class AggregateRoot<TId> : IInternalEventHandler
{
    private readonly List<IEvent> _changes = new();

    public TId Id { get; protected set; }

    public int Version { get; private set; } = -1;

    public IEnumerable<IEvent> Changes() => _changes;

    public void ClearChanges() => _changes.Clear();

    public void Load(IEnumerable<IEvent> history)
    {
        foreach (var e in history)
        {
            When(e);
            Version++;
        }
    }

    protected abstract void When(IEvent eventHappened);

    protected abstract void EnsureValidState();

    protected void Apply(IEvent @event)
    {
        When(eventHappened: @event);
        EnsureValidState();
        _changes.Add(@event);
    }

    protected void ApplyToEntity(IInternalEventHandler? entity, IEvent @event) =>
        entity?.Handle(@event);

    void IInternalEventHandler.Handle(IEvent @event) =>
        When(eventHappened: @event);
}
