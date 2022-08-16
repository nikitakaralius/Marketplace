using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace Marketplace.Infrastructure.Common;

public interface IRequestHandler
{
    Task<IActionResult> HandleCommandAsync<TRequest>(TRequest request, Func<TRequest, Task> handler, ILogger logger);

    Task<IActionResult> HandleQueryAsync<TResponse>(Func<Task<TResponse>> query, ILogger logger);

    IActionResult HandleQuery<TResponse>(Func<TResponse> query, ILogger logger);
}
