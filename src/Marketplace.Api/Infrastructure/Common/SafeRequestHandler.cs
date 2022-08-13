using static Microsoft.AspNetCore.Http.Results;
using ILogger = Serilog.ILogger;

namespace Marketplace.Infrastructure.Common;

internal sealed class SafeRequestHandler : IRequestHandler
{
    private readonly ILogger _logger;

    public SafeRequestHandler(ILogger logger) => _logger = logger;

    public async Task<IResult> HandleRequestAsync<TRequest>(TRequest request, Func<TRequest, Task> handler)
    {
        try
        {
            _logger.Debug("Handling HTTP request of type {Type}", typeof(TRequest).Name);
            await handler(request);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.Error(e, "Error handling the request");
            return BadRequest(new {error = e.Message, stackTrace = e.StackTrace});
        }
    }
}
