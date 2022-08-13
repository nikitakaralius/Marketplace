using Marketplace.Domain.Shared;
using Marketplace.Domain.UserProfile;
using Marketplace.Domain.UserProfile.ValueObjects;
using static Marketplace.UserProfiles.Contracts;

namespace Marketplace.UserProfiles;

internal sealed class UserProfilesApplicationService : IApplicationService<V1.ICommand>
{
    private readonly IUserProfileRepository _repository;
    private readonly IUnitOfWork _uow;
    private readonly CheckTextForProfanity _checkText;

    public UserProfilesApplicationService(IUserProfileRepository repository,
                                          IUnitOfWork uow,
                                          CheckTextForProfanity checkText)
    {
        _repository = repository;
        _uow = uow;
        _checkText = checkText;
    }

    public async Task HandleAsync(V1.ICommand command)
    {
        var _ = command switch
        {
            V1.RegisterUser r           => await RegisterUserAsync(r),
            V1.UpdateUserDisplayName r  => await UpdateAsync(r),
            V1.UpdateUserFullName r     => await UpdateAsync(r),
            V1.UpdateUserProfilePhoto r => await UpdateAsync(r),
            _                           => throw new ArgumentOutOfRangeException(nameof(command))
        };
    }

    private async Task<UserProfile> RegisterUserAsync(V1.RegisterUser request)
    {
        if (await _repository.ExistsAsync(request.Id))
        {
            throw new InvalidOperationException($"Entity with id {request.Id} already exists");
        }

        UserProfile profile = new(
            new(request.Id),
            FullName.FromString(request.FullName),
            DisplayName.FromString(request.DisplayName, _checkText));

        await _repository.AddAsync(profile);
        await _uow.CommitAsync();

        return profile;
    }

    private async Task<UserProfile> HandleUpdateAsync(Guid id, Action<UserProfile> operation)
    {
        var entity = await LoadAsync(id);
        operation(entity);
        await _uow.CommitAsync();
        return entity;
    }

    private async Task<UserProfile> LoadAsync(Guid entityId)
    {
        var entity = await _repository.LoadAsync(entityId);
        return entity ?? throw new InvalidOperationException($"Entity with {entityId} cannot be found");
    }

    private async Task<UserProfile> UpdateAsync(V1.UpdateUserDisplayName request) =>
        await HandleUpdateAsync(request.Id, profile =>
        {
            var displayName = DisplayName.FromString(request.DisplayName, _checkText);
            profile.UpdateDisplayName(displayName);
        });

    private async Task<UserProfile> UpdateAsync(V1.UpdateUserFullName request) =>
        await HandleUpdateAsync(request.Id, profile =>
        {
            var fullName = FullName.FromString(request.FullName);
            profile.UpdateFullName(fullName);
        });

    private async Task<UserProfile> UpdateAsync(V1.UpdateUserProfilePhoto request) =>
        await HandleUpdateAsync(request.Id, profile =>
        {
            var photoUrl = new Uri(request.PhotoUrl);
            profile.UpdateProfilePhoto(photoUrl);
        });
}
