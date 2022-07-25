namespace Marketplace.Domain.ValueObjects;

public sealed record ClassifiedAdTitle
{
    private readonly string _value;

    private ClassifiedAdTitle(string value)
    {
        if (value.Length > 100)
        {
            throw new ArgumentOutOfRangeException(
                nameof(value),
                "Title cannot be longer than 100 characters");
        }

        _value = value;
    }

    public static ClassifiedAdTitle FromString(string title) => new(title);
};
