namespace Marketplace.Domain.UserProfile.ValueObjects;

public sealed record FullName
{
    public readonly string Value = "";

    internal FullName(string value) => Value = value;

    public static FullName FromString(string fullName) =>
        string.IsNullOrWhiteSpace(fullName) == false ?
            new FullName(fullName)
            : throw new ArgumentNullException(nameof(fullName));

    public static implicit operator string(FullName fullName) => fullName.Value;

    public static readonly FullName None = new();

    private FullName() { }
}
