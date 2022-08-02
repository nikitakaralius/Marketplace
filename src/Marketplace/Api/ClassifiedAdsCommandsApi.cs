using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Api;

[ApiController]
[Route("/ad")]
public class ClassifiedAdsCommandsApi : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(ClassifiedAds.V1.Create request)
    {
        return Ok();
    }
}
