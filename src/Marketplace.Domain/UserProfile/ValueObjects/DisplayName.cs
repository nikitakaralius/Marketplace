using Marketplace.Domain.Shared;

namespace Marketplace.Domain.UserProfile.ValueObjects;

internal sealed record DisplayName
{
    public readonly string Value = "";

    internal DisplayName(string displayName) => Value = displayName;

    public static DisplayName FromString(string displayName, CheckTextForProfanity hasProfanity)
    {
        if (string.IsNullOrWhiteSpace(displayName))
            throw new ArgumentNullException(nameof(displayName));
        if (hasProfanity(displayName))
            throw new DomainException.ProfanityFound(displayName);
        return new DisplayName(displayName);
    }

    public static implicit operator string(DisplayName displayName) => displayName.Value;

    private DisplayName() { }
}
