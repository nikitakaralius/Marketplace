namespace Marketplace.Domain.ValueObjects;

public sealed record ClassifiedAdDescription
{
    internal ClassifiedAdDescription(string value) => Value = value;

    public static ClassifiedAdDescription FromString(string description) => new(description);

    public string Value { get; }

    public static implicit operator string(ClassifiedAdDescription description) => description.Value;
}
