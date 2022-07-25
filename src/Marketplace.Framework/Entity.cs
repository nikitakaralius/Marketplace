namespace Marketplace.Framework;

public abstract class Entity<T>
{
    private readonly List<IEvent<T>> _events;

    protected Entity() => _events = new List<IEvent<T>>();

    protected void Apply(IEvent<T> @event)
    {
        When(@event);
        EnsureValidState();
        _events.Add(@event);
    }

    public IEnumerable<IEvent<T>> Changes() => _events;

    public void ClearChanges() => _events.Clear();

    protected abstract void When(IEvent<T> @event);

    protected abstract void EnsureValidState();
}
