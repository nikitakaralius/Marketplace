using Marketplace.Domain.Shared;
using Marketplace.Domain.Shared.ValueObjects;

namespace Marketplace.Tests.ValueObjects;

public sealed class MoneyTests
{
    private sealed class FakeCurrencyLookup : ICurrencyLookup
    {
        public Currency FindCurrency(string currencyCode) =>
            currencyCode switch
            {
                "USD" => new() {Code = "USD", DecimalPlaces = 2},
                "EUR" => new() {Code = "EUR", DecimalPlaces = 2},
                "JPY" => new() {Code = "JPY", DecimalPlaces = 0},
                _     => Currency.None
            };
    }

    private static readonly ICurrencyLookup CurrencyLookup = new FakeCurrencyLookup();

    [Fact]
    public void MoneyWithTheSameAmountShouldBeEqual()
    {
        var first = Money.FromDecimal(5, "EUR", CurrencyLookup);
        var second = Money.FromDecimal(5, "EUR", CurrencyLookup);
        first.Should().Be(second);
        (first == second).Should().BeTrue();
        (first != second).Should().BeFalse();
    }

    [Fact]
    public void MoneyWithTheSameAmountButWithDifferentCurrenciesShouldNotBeEqual()
    {
        var first = Money.FromDecimal(5, "EUR", CurrencyLookup);
        var second = Money.FromDecimal(5, "USD", CurrencyLookup);
        first.Should().NotBe(second);
        (first == second).Should().BeFalse();
        (first != second).Should().BeTrue();
    }

    [Fact]
    public void SumOfMoneyGivesFullAmount()
    {
        var coin1 = Money.FromDecimal(1, "EUR", CurrencyLookup);
        var coin2 = Money.FromDecimal(2, "EUR", CurrencyLookup);
        var coin3 = Money.FromDecimal(2, "EUR", CurrencyLookup);
        var banknote = Money.FromDecimal(5, "EUR", CurrencyLookup);
        (coin1 + coin2 + coin3).Should().Be(banknote);
    }

    [Fact]
    public void SumOfMoneyWithDifferentCurrenciesShouldNotBeAllowed()
    {
        var eur = Money.FromDecimal(10, "EUR", CurrencyLookup);
        var usd = Money.FromDecimal(10, "USD", CurrencyLookup);
        var sum1 = () => eur + usd;
        var sum2 = () => eur.Add(usd);
        sum1.Should().ThrowExactly<DomainException.CurrencyMismatch>();
        sum2.Should().ThrowExactly<DomainException.CurrencyMismatch>();
    }

    [Fact]
    public void SubtractingMoneyWithDifferentCurrenciesShouldNotBeAllowed()
    {
        var eur = Money.FromDecimal(10, "EUR", CurrencyLookup);
        var usd = Money.FromDecimal(10, "USD", CurrencyLookup);
        var sub1 = () => eur - usd;
        var sub2 = () => eur.Subtract(usd);
        sub1.Should().ThrowExactly<DomainException.CurrencyMismatch>();
        sub2.Should().ThrowExactly<DomainException.CurrencyMismatch>();
    }

    [Fact]
    public void FromStringAndFromDecimalShouldBeEqual()
    {
        var fromDecimal = Money.FromDecimal(10, "EUR", CurrencyLookup);
        var fromString = Money.FromString("10.00", "EUR", CurrencyLookup);
        fromDecimal.Should().Be(fromString);
    }

    [Fact]
    public void CreatingMoneyWithMoreDecimalsThanCurrencyHasShouldNotBeAllowed()
    {
        var creating1 = () => Money.FromDecimal(10.12345667m, "EUR", CurrencyLookup);
        var creating2 = () => Money.FromString("10.124135", "EUR", CurrencyLookup);
        creating1.Should().Throw<Exception>();
        creating2.Should().Throw<Exception>();
    }

    [Fact]
    public void UsageOfUnknownCurrencyShouldNotBeAllowed()
    { var usage = () => Money.FromDecimal(123, "RUB", CurrencyLookup);
        usage.Should().Throw<Exception>();
    }
}
