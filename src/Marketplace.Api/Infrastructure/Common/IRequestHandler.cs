using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Infrastructure.Common;

public interface IRequestHandler
{
    Task<IActionResult> HandleRequestAsync<TRequest>(TRequest request, Func<TRequest, Task> handler);
}
