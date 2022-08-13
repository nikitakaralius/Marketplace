using static Microsoft.AspNetCore.Http.Results;

namespace Marketplace.Infrastructure.Common;

internal sealed class SafeRequestHandler : IRequestHandler
{
    private readonly ILogger<SafeRequestHandler> _logger;

    public SafeRequestHandler(ILogger<SafeRequestHandler> logger) => _logger = logger;

    public async Task<IResult> HandleRequestAsync<TRequest>(TRequest request, Func<TRequest, Task> handler)
    {
        try
        {
            _logger.LogDebug("Handling HTTP request of type {Type}", typeof(TRequest).Name);
            await handler(request);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error handling the request");
            return BadRequest(new {error = e.Message, stackTrace = e.StackTrace});
        }
    }
}
