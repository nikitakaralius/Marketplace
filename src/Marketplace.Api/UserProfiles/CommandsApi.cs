using Marketplace.Infrastructure.Common;
using static Marketplace.UserProfiles.Contracts;

namespace Marketplace.UserProfiles;

using Service = UserProfilesApplicationService;

internal static class CommandsApi
{
    public static void MapUserProfilesCommandsApi(this IEndpointRouteBuilder app)
    {
        app.MapPost("profile", async (IRequestHandler handler, Service service, V1.RegisterUser request) =>
                        await handler.HandleRequestAsync(request, service.HandleAsync));

        app.MapPut("profile/fullname", async (IRequestHandler handler, Service service, V1.UpdateUserFullName request) =>
                       await handler.HandleRequestAsync(request, service.HandleAsync));

        app.MapPut("profile/displayname", async (IRequestHandler handler, Service service, V1.UpdateUserDisplayName request) =>
                       await handler.HandleRequestAsync(request, service.HandleAsync));

        app.MapPut("profile/photo", async (IRequestHandler handler, Service service, V1.UpdateUserProfilePhoto request) =>
                       await handler.HandleRequestAsync(request, service.HandleAsync));
    }
}
