using System.Data.Common;
using System.Net;
using Marketplace.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.ClassifiedAds;

[ApiController, Route("/ad")]
public sealed class ClassifiedAdQueriesApi : ControllerBase
{
    private readonly DbConnection _connection;
    private readonly IRequestHandler _requestHandler;
    private readonly ILogger<ClassifiedAdQueriesApi> _logger;

    public ClassifiedAdQueriesApi(IRequestHandler requestHandler,
                                  ILogger<ClassifiedAdQueriesApi> logger,
                                  DbConnection connection)
    {
        _requestHandler = requestHandler;
        _logger = logger;
        _connection = connection;
    }

    [HttpGet]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    public async Task<IActionResult> Get(QueryModels.GetPublicClassifiedAd request) =>
        await _requestHandler.HandleQueryAsync(() => _connection.QueryAsync(request), _logger);

    [HttpGet("/list")]
    public async Task<IActionResult> Get(QueryModels.GetPublishedClassifiedAds request) =>
        await _requestHandler.HandleQueryAsync(() => _connection.QueryAsync(request), _logger);

    [HttpGet("/myads")]
    public async Task<IActionResult> Get(QueryModels.GetOwnersClassifiedAds request) =>
        await _requestHandler.HandleQueryAsync(() => _connection.QueryAsync(request), _logger);
}
