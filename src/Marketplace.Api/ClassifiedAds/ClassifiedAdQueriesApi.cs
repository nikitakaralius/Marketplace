using Marketplace.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.ClassifiedAds;

[ApiController, Route("/ad")]
public sealed class ClassifiedAdQueriesApi : ControllerBase
{
    private readonly ILogger<ClassifiedAdQueriesApi> _logger;
    private readonly IEnumerable<ReadModels.ClassifiedAdDetails> _items;
    private readonly IRequestHandler _handler;

    public ClassifiedAdQueriesApi(ILogger<ClassifiedAdQueriesApi> logger,
                                  IEnumerable<ReadModels.ClassifiedAdDetails> items,
                                  IRequestHandler handler)
    {
        _logger = logger;
        _items = items;
        _handler = handler;
    }

    [HttpGet]
    public IActionResult Get(QueryModels.GetPublicClassifiedAd request) =>
        _handler.HandleQuery(() => _items.Query(request), _logger);
}
