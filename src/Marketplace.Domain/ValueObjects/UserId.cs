namespace Marketplace.Domain.ValueObjects;

public sealed record UserId
{
    public readonly Guid Value;

    public UserId(Guid value)
    {
        if (value == default)
        {
            throw new ArgumentException("User id cannot be empty", nameof(value));
        }

        Value = value;
    }

    private UserId() { }

    public static readonly UserId None = new();

    public static implicit operator Guid(UserId id) => id.Value;
}
