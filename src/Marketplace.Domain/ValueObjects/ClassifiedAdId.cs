namespace Marketplace.Domain.ValueObjects;

public sealed record ClassifiedAdId
{
    private readonly Guid _value;

    public ClassifiedAdId(Guid value)
    {
        if (value == default)
        {
            throw new ArgumentException("Classified Ad id cannot be empty", nameof(value));
        }

        _value = value;
    }

    public static implicit operator Guid(ClassifiedAdId id) => id._value;
}
