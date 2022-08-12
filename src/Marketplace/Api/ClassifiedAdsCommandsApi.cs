using Microsoft.AspNetCore.Mvc;
using static Marketplace.Contracts.ClassifiedAds;

namespace Marketplace.Api;

[ApiController, Route("/ad")]
public class ClassifiedAdsCommandsApi : ControllerBase
{
    private readonly ClassifiedAdsApplicationService _applicationService;

    public ClassifiedAdsCommandsApi(ClassifiedAdsApplicationService applicationService)
    {
        // throw new InvalidOperationException("Minimal APIs refactoring required");
        _applicationService = applicationService;
    }

    [HttpPost]
    public async Task<IActionResult> Post(V1.Create request)
    {
        await _applicationService.HandleAsync(request);
        return Ok();
    }

    [HttpPut, Route("title")]
    public async Task<IActionResult> Put(V1.SetTitle request)
    {
        await _applicationService.HandleAsync(request);
        return Ok();
    }

    [HttpPut, Route("description")]
    public async Task<IActionResult> Put(V1.UpdateDescription request)
    {
        await _applicationService.HandleAsync(request);
        return Ok();
    }

    [HttpPut, Route("price")]
    public async Task<IActionResult> Put(V1.UpdatePrice request)
    {
        await _applicationService.HandleAsync(request);
        return Ok();
    }

    [HttpPut, Route("publish")]
    public async Task<IActionResult> Put(V1.RequestToPublish request)
    {
        await _applicationService.HandleAsync(request);
        return Ok();
    }
}
