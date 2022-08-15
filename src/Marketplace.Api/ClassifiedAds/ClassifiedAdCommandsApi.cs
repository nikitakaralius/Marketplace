using Marketplace.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;
using static Marketplace.ClassifiedAds.ClassifiedAdContract;
using ILogger = Serilog.ILogger;

namespace Marketplace.ClassifiedAds;

[ApiController, Route("ad")]
public sealed class ClassifiedAdCommandsApi : ControllerBase
{
    private static readonly ILogger Logger = Log.ForContext<ClassifiedAdCommandsApi>();

    private readonly IRequestHandler _handler;
    private readonly ClassifiedAdsApplicationService _service;

    public ClassifiedAdCommandsApi(IRequestHandler handler, ClassifiedAdsApplicationService service)
    {
        _handler = handler;
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Post(V1.Create request) =>
        await _handler.HandleCommandAsync(request, _service.HandleAsync, Logger);

    [HttpPut("title")]
    public async Task<IActionResult> Put(V1.SetTitle request) =>
        await _handler.HandleCommandAsync(request, _service.HandleAsync, Logger);

    [HttpPut("description")]
    public async Task<IActionResult> Put(V1.UpdateDescription request) =>
        await _handler.HandleCommandAsync(request, _service.HandleAsync, Logger);

    [HttpPut("price")]
    public async Task<IActionResult> Put(V1.UpdatePrice request) =>
        await _handler.HandleCommandAsync(request, _service.HandleAsync, Logger);

    [HttpPut("publish")]
    public async Task<IActionResult> Put(V1.RequestToPublish request) =>
        await _handler.HandleCommandAsync(request, _service.HandleAsync, Logger);
}
