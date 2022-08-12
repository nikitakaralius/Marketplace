namespace Marketplace.Domain.ValueObjects;

public sealed record ClassifiedAdTitle
{
    public readonly string Value;

    private ClassifiedAdTitle(string value)
    {
        CheckValidity(value);
        Value = value;
    }

    private static void CheckValidity(string value)
    {
        if (value.Length > 100)
        {
            throw new ArgumentOutOfRangeException(
                nameof(value),
                "Title cannot be longer than 100 characters");
        }
    }

    public static ClassifiedAdTitle FromString(string title) => new(title);

    public static implicit operator string(ClassifiedAdTitle title) => title.Value;
};
