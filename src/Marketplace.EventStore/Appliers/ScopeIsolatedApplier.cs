using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.EventStore.Appliers;

internal abstract class ScopeIsolatedApplier<T> : IApplyOn<T> where T : notnull
{
    private readonly IServiceScopeFactory _factory;

    protected ScopeIsolatedApplier(IServiceScopeFactory factory) =>
        _factory = factory;

    public async Task ApplyAsync(Func<T, Task> action)
    {
        await using var scope = _factory.CreateAsyncScope();
        var service = scope.ServiceProvider.GetRequiredService<T>();
        await action(service);
    }

    public async Task<TResult> ReceiveAsync<TResult>(Func<T, Task<TResult>> action)
    {
        await using var scope = _factory.CreateAsyncScope();
        var service = scope.ServiceProvider.GetRequiredService<T>();
        return await action(service);
    }
}
