using Marketplace.Domain.Shared;
using Marketplace.Domain.Shared.ValueObjects;

namespace Marketplace.Domain.ClassifiedAd.ValueObjects;

public sealed record Price : Money
{
    private Price(decimal amount, string currencyCode, ICurrencyLookup currencyLookup)
        : base(amount, currencyCode, currencyLookup)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Price cannot be negative", nameof(amount));
        }
    }

    internal Price(decimal amount, string currencyCode) : base(amount, new Currency {Code = currencyCode}) { }

    private Price() { }

    public static readonly Price None = new();

    public new static Price FromDecimal(decimal amount, string currency, ICurrencyLookup currencyLookup) =>
        new(amount, currency, currencyLookup);
}
