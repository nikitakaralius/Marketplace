namespace Marketplace.Infrastructure.Persistence;

public interface IAggregateStore
{
    Task<bool> ExistsAsync<TAggregate, TId>(TId aggregateId)
        where TAggregate : AggregateRoot<TId>
        where TId : notnull;

    Task SaveAsync<TAggregate, TId>(TAggregate aggregate)
        where TAggregate : AggregateRoot<TId>
        where TId : notnull;

    Task<TAggregate> LoadAsync<TAggregate, TId>(TId aggregateId)
        where TAggregate : AggregateRoot<TId>
        where TId : notnull;
}
