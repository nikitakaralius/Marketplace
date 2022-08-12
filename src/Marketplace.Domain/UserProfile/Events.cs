using Marketplace.Framework;

namespace Marketplace.Domain.UserProfile;

using UserEvent = IEvent<UserProfile>;

public static class Events
{
    public class UserRegistered : UserEvent
    {
        public Guid UserId { get; init; }
        public string FullName { get; init; } = null!;
        public string DisplayName { get; init; } = null!;
    }

    public class ProfilePhotoUpdated : UserEvent
    {
        public Guid UserId { get; init; }
        public string PhotoUrl { get; init; } = null!;
    }

    public class UserFullNameUpdated : UserEvent
    {
        public Guid UserId { get; init; }
        public string FullName { get; init; } = null!;
    }

    public class UserDisplayNameUpdated : UserEvent
    {
        public Guid UserId { get; init; }
        public string DisplayName { get; init; } = null!;
    }
}
