using static Marketplace.Contracts.ClassifiedAds;
using static Microsoft.AspNetCore.Http.Results;

namespace Marketplace.Api;

using Handler = ClassifiedAdsApplicationService;

public static class ClassifiedAdsCommandsApi
{
    public static void MapClassifiedAdsCommandsApi(this IEndpointRouteBuilder app)
    {
        app.MapPost("ad", async (Handler handler, V1.Create request) =>
        {
            await handler.HandleAsync(request);
            return Ok();
        });

        app.MapPut("ad/title", async (Handler handler, V1.SetTitle request) =>
        {
            await handler.HandleAsync(request);
            return Ok();
        });

        app.MapPut("ad/description", async (Handler handler, V1.UpdateDescription request) =>
        {
            await handler.HandleAsync(request);
            return Ok();
        });

        app.MapPut("ad/price", async (Handler handler, V1.UpdatePrice request) =>
        {
            await handler.HandleAsync(request);
            return Ok();
        });

        app.MapPut("ad/publish", async (Handler handler, V1.RequestToPublish request) =>
        {
            await handler.HandleAsync(request);
            return Ok();
        });
    }
}
