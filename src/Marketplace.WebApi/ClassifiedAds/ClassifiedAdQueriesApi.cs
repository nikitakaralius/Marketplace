using Marketplace.ClassifiedAds.Projections;
using Marketplace.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace Marketplace.WebApi.ClassifiedAds;

[ApiController, Route("/ad")]
public sealed class ClassifiedAdQueriesApi : ControllerBase
{
    private static readonly ILogger Logger = Log.ForContext<ClassifiedAdQueriesApi>();

    private readonly IClassifiedAdRepository _repository;
    private readonly IRequestHandler _handler;

    public ClassifiedAdQueriesApi(IClassifiedAdRepository repository, IRequestHandler handler)
    {
        _repository = repository;
        _handler = handler;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] QueryModels.GetPublicClassifiedAd request) =>
        await _handler.HandleQueryAsync(() => _repository.ByIdAsync(request.ClassifiedAdId), Logger);
}
