namespace Marketplace.Tests.ValueObjects;

public sealed class PriceTests
{
    private sealed class FakeCurrencyLookup : ICurrencyLookup
    {
        public CurrencyDetails FindCurrency(string currencyCode) =>
            currencyCode switch
            {
                "USD" => new() {CurrencyCode = "USD", DecimalPlaces = 2},
                "EUR" => new() {CurrencyCode = "EUR", DecimalPlaces = 2},
                "JPY" => new() {CurrencyCode = "JPY", DecimalPlaces = 0},
                _     => CurrencyDetails.None
            };
    }

    private static readonly ICurrencyLookup CurrencyLookup = new FakeCurrencyLookup();

    [Fact]
    public void PriceObjectsWithTheSameAmountShouldBeEqual()
    {
        Price first = Price.FromDecimal(5, "EUR", CurrencyLookup);
        Price second = Price.FromDecimal(5, "EUR", CurrencyLookup);
        first.Should().Be(second);
        (first == second).Should().BeTrue();
        (first != second).Should().BeFalse();
    }

    [Fact]
    public void ThrowsExceptionWhenInitializingPriceWithNegativeAmount()
    {
        var initializing = () => Price.FromDecimal(-5, "EUR", CurrencyLookup);
        initializing.Should().Throw<ArgumentException>();
    }
}
