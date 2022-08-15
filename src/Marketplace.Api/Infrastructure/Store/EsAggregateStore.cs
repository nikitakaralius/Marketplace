using System.Text.Json;

namespace Marketplace.Infrastructure.Store;

internal sealed class EsAggregateStore : IAggregateStore
{
    public Task<bool> ExistsAsync<TAggregate, TId>(TId aggregateId) where TAggregate : AggregateRoot<TId>
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync<TAggregate, TId>(TAggregate aggregate) where TAggregate : AggregateRoot<TId>
    {
        throw new NotImplementedException();
    }

    public Task<TAggregate> LoadAsync<TAggregate, TId>(TId aggregateId) where TAggregate : AggregateRoot<TId>
    {
        throw new NotImplementedException();
    }

    private static string StreamName<TAggregate, TId>(TId aggregateId) where TId : notnull =>
        $"{typeof(TAggregate).Name}-{aggregateId.ToString()}";

    private static string StreamName<TAggregate, TId>(TAggregate aggregate)
        where TAggregate : AggregateRoot<TId>
        where TId : notnull =>
        $"{typeof(TAggregate).Name}-{aggregate.Id.ToString()}";

    private static byte[] Serialize(object data) =>
        JsonSerializer.SerializeToUtf8Bytes(data);
}
