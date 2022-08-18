namespace Marketplace.EventSourcing;

public interface IApplyOn<out T> where T : notnull
{
    Task ApplyAsync(Func<T, Task> action);

    Task<TResult> ReceiveAsync<TResult>(Func<T, Task<TResult>> action);
}
