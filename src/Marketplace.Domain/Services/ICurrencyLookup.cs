namespace Marketplace.Domain.Services;

public interface ICurrencyLookup
{
    CurrencyDetails FindCurrency(string currencyCode);
}

public sealed class CurrencyDetails
{
    public string CurrencyCode { get; init; } = "";

    public bool InUse { get; init; } = true;

    public int DecimalPlaces { get; init; }

    public static CurrencyDetails None => new() {InUse = false};
}
