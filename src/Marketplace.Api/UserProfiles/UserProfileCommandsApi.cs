using Marketplace.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;
using static Marketplace.UserProfiles.Contracts;

namespace Marketplace.UserProfiles;

[ApiController, Route("profile")]
public sealed class UserProfileCommandsApi : ControllerBase
{
    private readonly IRequestHandler _handler;
    private readonly UserProfilesApplicationService _service;

    public UserProfileCommandsApi(IRequestHandler handler, UserProfilesApplicationService service)
    {
        _handler = handler;
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Post(V1.RegisterUser request) =>
        await _handler.HandleRequestAsync(request, _service.HandleAsync);

    [HttpPut("fullname")]
    public async Task<IActionResult> Put(V1.UpdateUserFullName request) =>
        await _handler.HandleRequestAsync(request, _service.HandleAsync);

    [HttpPut("displayname")]
    public async Task<IActionResult> Put(V1.UpdateUserDisplayName request) =>
        await _handler.HandleRequestAsync(request, _service.HandleAsync);

    [HttpPut("photo")]
    public async Task<IActionResult> Put(V1.UpdateUserProfilePhoto request) =>
        await _handler.HandleRequestAsync(request, _service.HandleAsync);
}
