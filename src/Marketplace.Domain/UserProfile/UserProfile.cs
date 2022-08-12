using Marketplace.Domain.UserProfile.ValueObjects;
using Marketplace.Framework;
using static Marketplace.Domain.UserProfile.Events;

namespace Marketplace.Domain.UserProfile;

public sealed class UserProfile : AggregateRoot
{
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

    protected override void When(IEvent eventHappened)
    {

    }

    protected override void EnsureValidState()
    {
    }
}
