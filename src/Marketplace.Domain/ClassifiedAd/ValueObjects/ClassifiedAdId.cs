namespace Marketplace.Domain.ClassifiedAd.ValueObjects;

public sealed record ClassifiedAdId
{
    public readonly Guid Value;

    public ClassifiedAdId(Guid value)
    {
        if (value == default)
        {
            throw new ArgumentException("Classified Ad id cannot be empty", nameof(value));
        }

        Value = value;
    }

    private ClassifiedAdId() { }

    public static implicit operator Guid(ClassifiedAdId id) => id.Value;

    public static implicit operator ClassifiedAdId(string value) => new(Guid.Parse(value));

    public override string ToString() => Value.ToString();
}
