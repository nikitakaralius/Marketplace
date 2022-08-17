using Marketplace.Domain.UserProfile.ValueObjects;
using Marketplace.Infrastructure;
using static Marketplace.Domain.UserProfile.Events;

namespace Marketplace.Domain.UserProfile;

using UserEvent = IEvent<UserProfile>;

public sealed class UserProfile : AggregateRoot<UserId>
{
    private UserProfile()
    {
    }

    public Guid DatabaseId { get; private set; }

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
            UserRegistered e => () =>
            {
                DatabaseId = e.UserId;
                Id = new(e.UserId);
                FullName = new(e.FullName);
                DisplayName = new(e.DisplayName);
            },
            UserFullNameUpdated e => () =>
            {
                FullName = new(e.FullName);
            },
            UserDisplayNameUpdated e => () =>
            {
                DisplayName = new(e.DisplayName);
            },
            ProfilePhotoUpdated e => () =>
            {
                PhotoUrl = new(e.PhotoUrl);
            },
            _                        => throw new ArgumentOutOfRangeException(nameof(@event))
        };
        when();
    }

    protected override void EnsureValidState()
    {
    }
}
