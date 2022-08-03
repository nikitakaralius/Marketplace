using Microsoft.AspNetCore.Mvc;
using static Marketplace.Contracts.ClassifiedAds;

namespace Marketplace.Api;

[ApiController, Route("/ad")]
public class ClassifiedAdsCommandsApi : ControllerBase
{
    private readonly ClassifiedAdsApplicationService _applicationService;

    public ClassifiedAdsCommandsApi(ClassifiedAdsApplicationService applicationService) =>
        _applicationService = applicationService;

    [HttpPost]
    public async Task<IActionResult> Post(V1.Create request)
    {
        _applicationService.Handle(request);
        return Ok();
    }
}
