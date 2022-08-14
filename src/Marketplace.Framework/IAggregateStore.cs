namespace Marketplace.Framework;

public interface IAggregateStore
{
    Task<bool> ExistsAsync<TAggregate, TId>(TId aggregateId);

    Task SaveAsync<TAggregate>(TAggregate aggregate) where TAggregate : AggregateRoot;

    Task<TAggregate> LoadAsync<TAggregate, TId>(TId aggregateId);
}
