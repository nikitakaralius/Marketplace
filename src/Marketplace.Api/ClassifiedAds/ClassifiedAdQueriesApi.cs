using Marketplace.Infrastructure.Common;
using Marketplace.Projections;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace Marketplace.ClassifiedAds;

[ApiController, Route("/ad")]
public sealed class ClassifiedAdQueriesApi : ControllerBase
{
    private static readonly ILogger Logger = Log.ForContext<ClassifiedAdQueriesApi>();

    private readonly IEnumerable<ReadModels.ClassifiedAdDetails> _items;
    private readonly IRequestHandler _handler;

    public ClassifiedAdQueriesApi(IEnumerable<ReadModels.ClassifiedAdDetails> items,
                                  IRequestHandler handler)
    {
        _items = items;
        _handler = handler;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] QueryModels.GetPublicClassifiedAd request) =>
        _handler.HandleQuery(() => _items.Query(request), Logger);
}
