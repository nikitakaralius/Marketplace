namespace Marketplace.Framework;

public interface IAggregateStore
{
    Task<bool> ExistsAsync<TAggregate, TId>(TId aggregateId) where TAggregate : AggregateRoot<TId>;

    Task SaveAsync<TAggregate, TId>(TAggregate aggregate) where TAggregate : AggregateRoot<TId>;

    Task<TAggregate> LoadAsync<TAggregate, TId>(TId aggregateId) where TAggregate : AggregateRoot<TId>;
}
