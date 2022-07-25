namespace Marketplace.Domain.Services;

public interface ICurrencyLookup
{
    Currency FindCurrency(string currencyCode);
}

public sealed class Currency
{
    public string Code { get; init; } = "";

    public bool InUse { get; init; } = true;

    public int DecimalPlaces { get; init; }

    public static Currency None => new() {InUse = false};
}
