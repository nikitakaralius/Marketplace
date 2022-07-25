namespace Marketplace.Framework;

public abstract class Entity<T>
{
    private readonly List<IEvent<T>> _events;

    protected Entity() => _events = new List<IEvent<T>>();

    protected void Raise(IEvent<T> @event) => _events.Add(@event);

    public IEnumerable<IEvent<T>> Changes() => _events;

    public void ClearChanges() => _events.Clear();
}
