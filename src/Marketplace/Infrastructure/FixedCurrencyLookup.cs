using Marketplace.Domain.Shared;

namespace Marketplace.Infrastructure;

internal sealed class FixedCurrencyLookup : ICurrencyLookup
{
    public Currency FindCurrency(string currencyCode)
    {
        return currencyCode switch
        {
            "EUR" => new Currency
            {
                Code = "EUR",
                DecimalPlaces = 2,
                InUse = true
            },
            "USD" => new Currency
            {
                Code = "USD",
                DecimalPlaces = 2,
                InUse = true
            },
            "RUB" => new Currency
            {
                Code = "RUB",
                DecimalPlaces = 2,
                InUse = true
            },
            _ => Currency.None
        };
    }
}
