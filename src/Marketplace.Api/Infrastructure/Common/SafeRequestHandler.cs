using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Infrastructure.Common;

internal sealed class SafeRequestHandler : IRequestHandler
{
    public async Task<IActionResult> HandleCommandAsync<TRequest>(TRequest request, Func<TRequest, Task> handler, ILogger logger)
    {
        try
        {
            logger.LogDebug("Handling HTTP request of type {Type}", typeof(TRequest).Name);
            await handler(request);
            return new OkResult();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error handling the request");
            return new BadRequestObjectResult(new {error = e.Message, stackTrace = e.StackTrace});
        }
    }

    public async Task<IActionResult> HandleQueryAsync<TResponse>(Func<Task<TResponse>> query, ILogger logger)
    {
        try
        {
            return new OkObjectResult(await query());
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error handling the query");
            return new BadRequestObjectResult(new
            {
                error = e.Message,
                stackTrace = e.StackTrace
            });
        }
    }

    public IActionResult HandleQuery<TResponse>(Func<TResponse> query, ILogger logger)
    {
        try
        {
            return new OkObjectResult(query());
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error handling the query");
            return new BadRequestObjectResult(new
            {
                error = e.Message,
                stackTrace = e.StackTrace
            });
        }
    }
}
