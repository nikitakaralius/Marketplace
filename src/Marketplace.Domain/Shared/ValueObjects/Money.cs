using Marketplace.Domain.Shared.Exceptions;

namespace Marketplace.Domain.Shared.ValueObjects;

public record Money
{
    public readonly decimal Amount;

    public readonly Currency Currency = null!;

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
        Currency = currency;
    }

    protected Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    protected Money() { }

    public static Money FromDecimal(decimal amount, string currency, ICurrencyLookup currencyLookup) =>
        new(amount, currency, currencyLookup);

    public static Money FromString(string amount, string currency, ICurrencyLookup currencyLookup) =>
        new(decimal.Parse(amount), currency, currencyLookup);

    public Money Add(Money summand)
    {
        if (Currency != summand.Currency)
        {
            throw new CurrencyMismatchException("Cannot sum amounts with different currencies");
        }

        return new Money(Amount + summand.Amount, Currency);
    }

    public Money Subtract(Money subtrahend)
    {
        if (Currency != subtrahend.Currency)
        {
            throw new CurrencyMismatchException("Cannot subtract amounts with different currencies");
        }

        return new Money(Amount - subtrahend.Amount, Currency);
    }

    public static Money operator +(Money firstSummand, Money secondSummand) => firstSummand.Add(secondSummand);

    public static Money operator -(Money minuend, Money subtrahend) => minuend.Subtract(subtrahend);

    public override string ToString() => $"{Currency} {Amount}";
}
