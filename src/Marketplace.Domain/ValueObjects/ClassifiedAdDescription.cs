namespace Marketplace.Domain.ValueObjects;

public sealed record ClassifiedAdDescription
{
    public readonly string Value;

    internal ClassifiedAdDescription(string value) => Value = value;

    public static ClassifiedAdDescription FromString(string description) => new(description);
    
    public static implicit operator string(ClassifiedAdDescription description) => description.Value;
}
