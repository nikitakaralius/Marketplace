using Marketplace.Domain.Services;

namespace Marketplace.Domain.ValueObjects;

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

    public new static Price FromDecimal(decimal amount, string currency, ICurrencyLookup currencyLookup) =>
        new(amount, currency, currencyLookup);
}
