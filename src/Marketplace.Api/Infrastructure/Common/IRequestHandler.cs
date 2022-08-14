using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Infrastructure.Common;

public interface IRequestHandler
{
    Task<IActionResult> HandleCommandAsync<TRequest>(TRequest request, Func<TRequest, Task> handler, ILogger logger);

    Task<IActionResult> HandleQueryAsync<TResponse>(Func<Task<TResponse>> query, ILogger logger);
}
