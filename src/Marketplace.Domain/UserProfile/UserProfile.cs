using Marketplace.Domain.UserProfile.ValueObjects;
using Marketplace.Framework;
using static Marketplace.Domain.UserProfile.Events;

namespace Marketplace.Domain.UserProfile;

using UserEvent = IEvent<UserProfile>;

public sealed class UserProfile : AggregateRoot
{
    private UserProfile()
    {
    }

    public Guid DatabaseId { get; private set; }

    public UserId Id { get; private set; } = null!;

    public FullName FullName { get; private set; } = FullName.None;

    public DisplayName DisplayName { get; private set; } = DisplayName.None;

    public Uri? PhotoUrl { get; private set; }

    public UserProfile(UserId id, FullName fullName, DisplayName displayName) =>
        Apply(new UserRegistered
        {
            UserId = id,
            FullName = fullName,
            DisplayName = displayName
        });

    public void UpdateFullName(FullName fullName) =>
        Apply(new UserFullNameUpdated
        {
            UserId = Id,
            FullName = fullName
        });

    public void UpdateDisplayName(DisplayName displayName) =>
        Apply(new UserDisplayNameUpdated
        {
            UserId = Id,
            DisplayName = displayName
        });

    public void UpdateProfilePhoto(Uri photoUrl) =>
        Apply(new ProfilePhotoUpdated
        {
            UserId = Id,
            PhotoUrl = photoUrl.ToString()
        });

    protected override void When(IEvent eventHappened)
    {
        var @event = eventHappened as UserEvent;
        Action when = @event switch
        {
            ProfilePhotoUpdated e    => () => { },
            UserDisplayNameUpdated e => () => { },
            UserFullNameUpdated e    => () => { },
            UserRegistered e         => () => { },
            _                        => throw new ArgumentOutOfRangeException(nameof(@event))
        };
    }

    protected override void EnsureValidState()
    {
    }
}
