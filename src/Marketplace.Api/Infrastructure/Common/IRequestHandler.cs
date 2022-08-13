namespace Marketplace.Infrastructure.Common;

internal interface IRequestHandler
{
    Task<IResult> HandleRequestAsync<TRequest>(TRequest request, Func<TRequest, Task> handler);
}
