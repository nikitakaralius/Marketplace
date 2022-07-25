using Marketplace.Domain.Exceptions;
using Marketplace.Domain.Services;

namespace Marketplace.Domain.ValueObjects;

public record Money
{
    protected Money(decimal amount, string currencyCode, ICurrencyLookup currencyLookup)
    {
        if (string.IsNullOrEmpty(currencyCode))
        {
            throw new ArgumentNullException(
                nameof(currencyCode),
                "Currency code must be specified");
        }

        var currency = currencyLookup.FindCurrency(currencyCode);

        if (currency.InUse == false)
        {
            throw new ArgumentException($"Currency {currencyCode} is not valid");
        }

        if (decimal.Round(amount, currency.DecimalPlaces) != amount)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                $"Amount cannot have more than {currency.DecimalPlaces} decimals");
        }

        Amount = amount;
        CurrencyCode = currencyCode;
    }

    private Money(decimal amount, string currencyCode)
    {
        Amount = amount;
        CurrencyCode = currencyCode;
    }

    public static Money FromDecimal(decimal amount, string currency, ICurrencyLookup currencyLookup) =>
        new(amount, currency, currencyLookup);

    public static Money FromString(string amount, string currency, ICurrencyLookup currencyLookup) =>
        new(decimal.Parse(amount), currency, currencyLookup);

    public decimal Amount { get; }

    public string CurrencyCode { get; }

    public Money Add(Money summand)
    {
        if (CurrencyCode != summand.CurrencyCode)
        {
            throw new CurrencyMismatchException("Cannot sum amounts with different currencies");
        }

        return new Money(Amount + summand.Amount, CurrencyCode);
    }

    public Money Subtract(Money subtrahend)
    {
        if (CurrencyCode != subtrahend.CurrencyCode)
        {
            throw new CurrencyMismatchException("Cannot subtract amounts with different currencies");
        }

        return new Money(Amount - subtrahend.Amount, CurrencyCode);
    }

    public static Money operator +(Money firstSummand, Money secondSummand) => firstSummand.Add(secondSummand);

    public static Money operator -(Money minuend, Money subtrahend) => minuend.Subtract(subtrahend);

    public override string ToString() => $"{CurrencyCode} {Amount}";
}
