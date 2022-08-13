using Marketplace.Domain.Shared;

namespace Marketplace.Domain.UserProfile.ValueObjects;

public sealed record DisplayName
{
    public readonly string Value = "";

    internal DisplayName(string displayName) => Value = displayName;

    public static DisplayName FromString(string displayName, IContentModeration contentModeration)
    {
        if (string.IsNullOrWhiteSpace(displayName))
            throw new ArgumentNullException(nameof(displayName));
        if (contentModeration.CheckForProfanityAsync(displayName))
            throw new DomainException.ProfanityFound(displayName);
        return new DisplayName(displayName);
    }

    public static implicit operator string(DisplayName displayName) => displayName.Value;

    public static readonly DisplayName None = new();

    private DisplayName() { }
}
