namespace Marketplace.Domain.ValueObjects;

public sealed record ClassifiedAdDescription
{
    public readonly string Value = null!;

    internal ClassifiedAdDescription(string value) => Value = value;

    private ClassifiedAdDescription() { }

    public static ClassifiedAdDescription FromString(string description) => new(description);

    public static implicit operator string(ClassifiedAdDescription description) => description.Value;
}
