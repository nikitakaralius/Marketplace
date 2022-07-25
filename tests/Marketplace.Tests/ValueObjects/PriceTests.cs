namespace Marketplace.Tests.ValueObjects;

public sealed class PriceTests
{
    [Fact]
    public void PriceObjectsWithTheSameAmountShouldBeEqual()
    {
        Price first = new(5);
        Price second = new(5);
        first.Should().Be(second);
        (first == second).Should().BeTrue();
        (first != second).Should().BeFalse();
    }

    [Fact]
    public void ThrowsExceptionWhenInitializingPriceWithNegativeAmount()
    {
        var initializing = () => new Price(-5);
        initializing.Should().Throw<ArgumentException>();
    }
}
