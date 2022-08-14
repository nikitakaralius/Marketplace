using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.ClassifiedAds;

[ApiController, Route("/ad")]
public sealed class ClassifiedAdQueriesApi : ControllerBase
{
    [HttpGet]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    public async Task<IActionResult> Get(QueryModels.GetPublicClassifiedAd request)
    {
    }

    [HttpGet("/list")]
    public async Task<IActionResult> Get(QueryModels.GetPublishedClassifiedAds request)
    {
    }

    [HttpGet("/myads")]
    public async Task<IActionResult> Get(QueryModels.GetOwnersClassifiedAds request)
    {
    }
}
