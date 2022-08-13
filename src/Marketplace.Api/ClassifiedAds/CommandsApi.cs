using Marketplace.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;
using static Marketplace.ClassifiedAds.ClassifiedAdContract;

namespace Marketplace.ClassifiedAds;

[ApiController, Route("ad")]
public class CommandsApi : ControllerBase
{
    private readonly IRequestHandler _handler;
    private readonly ClassifiedAdsApplicationService _service;

    public CommandsApi(IRequestHandler handler, ClassifiedAdsApplicationService service)
    {
        _handler = handler;
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Post(V1.Create request) =>
        await _handler.HandleRequestAsync(request, _service.HandleAsync);

    [HttpPut("title")]
    public async Task<IActionResult> Put(V1.SetTitle request) =>
        await _handler.HandleRequestAsync(request, _service.HandleAsync);

    [HttpPut("description")]
    public async Task<IActionResult> Put(V1.UpdateDescription request) =>
        await _handler.HandleRequestAsync(request, _service.HandleAsync);

    [HttpPut("price")]
    public async Task<IActionResult> Put(V1.UpdatePrice request) =>
        await _handler.HandleRequestAsync(request, _service.HandleAsync);

    [HttpPut("publish")]
    public async Task<IActionResult> Put(V1.RequestToPublish request) =>
        await _handler.HandleRequestAsync(request, _service.HandleAsync);
}
