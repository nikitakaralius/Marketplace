using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Infrastructure.Common;

public interface IRequestHandler
{
    Task<IActionResult> HandleCommandAsync<TRequest>(TRequest request, Func<TRequest, Task> handler);
}
