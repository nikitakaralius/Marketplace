using Marketplace.Infrastructure.Common;
using static Marketplace.ClassifiedAds.ClassifiedAdContract;

namespace Marketplace.ClassifiedAds;

using Service = ClassifiedAdsApplicationService;

public static class CommandsApi
{
    public static void MapClassifiedAdsCommandsApi(this IEndpointRouteBuilder app)
    {
        app.MapPost("ad", async (IRequestHandler handler, Service service, V1.Create request) =>
                        await handler.HandleRequestAsync(request, service.HandleAsync));

        app.MapPut("ad/title", async (IRequestHandler handler, Service service, V1.SetTitle request) =>
                       await handler.HandleRequestAsync(request, service.HandleAsync));

        app.MapPut("ad/description", async (IRequestHandler handler, Service service, V1.UpdateDescription request) =>
                       await handler.HandleRequestAsync(request, service.HandleAsync));

        app.MapPut("ad/price", async (IRequestHandler handler, Service service, V1.UpdatePrice request) =>
                       await handler.HandleRequestAsync(request, service.HandleAsync));

        app.MapPut("ad/publish", async (IRequestHandler handler, Service service, V1.RequestToPublish request) =>
                       await handler.HandleRequestAsync(request, service.HandleAsync));
    }
}
