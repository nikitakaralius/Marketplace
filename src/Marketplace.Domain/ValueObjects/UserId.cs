namespace Marketplace.Domain.ValueObjects;

public sealed record UserId
{
    private readonly Guid _value;

    public UserId(Guid value)
    {
        if (value == default)
        {
            throw new ArgumentException("User id cannot be empty", nameof(value));
        }

        _value = value;
    }

    public static implicit operator Guid(UserId id) => id._value;
}
