namespace Marketplace.Framework;

public abstract class Entity<TEntity>
{
    private readonly List<IEvent<TEntity>> _changes = new();

    protected void Apply(IEvent<TEntity> @event)
    {
        When(@event);
        EnsureValidState();
        _changes.Add(@event);
    }

    public IEnumerable<IEvent<TEntity>> Changes() => _changes;

    public void ClearChanges() => _changes.Clear();

    protected abstract void When(IEvent<TEntity> @event);

    protected abstract void EnsureValidState();
}
