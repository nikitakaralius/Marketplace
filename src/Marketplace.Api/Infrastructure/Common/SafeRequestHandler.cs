using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Infrastructure.Common;

internal sealed class SafeRequestHandler : IRequestHandler
{
    private readonly ILogger<SafeRequestHandler> _logger;

    public SafeRequestHandler(ILogger<SafeRequestHandler> logger) => _logger = logger;

    public async Task<IActionResult> HandleRequestAsync<TRequest>(TRequest request, Func<TRequest, Task> handler)
    {
        try
        {
            _logger.LogDebug("Handling HTTP request of type {Type}", typeof(TRequest).Name);
            await handler(request);
            return new OkResult();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error handling the request");
            return new BadRequestObjectResult(new {error = e.Message, stackTrace = e.StackTrace});
        }
    }
}
