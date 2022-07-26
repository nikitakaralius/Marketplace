namespace Marketplace.Domain.Shared;

public interface ICurrencyLookup
{
    Currency FindCurrency(string currencyCode);
}

public sealed record Currency
{
    public string Code { get; init; } = "";

    public bool InUse { get; init; } = true;

    public int DecimalPlaces { get; init; }

    public static Currency None => new() {InUse = false};
}
