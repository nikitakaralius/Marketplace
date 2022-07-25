namespace Marketplace.Tests.ValueObjects;

public sealed class MoneyTests
{
    [Fact]
    public void MoneyObjectsWithTheSameAmountShouldBeEqual()
    {
        Money first = new(5);
        Money second = new(5);
        first.Should().Be(second);
        (first == second).Should().BeTrue();
        (first != second).Should().BeFalse();
    }

    [Fact]
    public void SumOfMoneyGivesFullAmount()
    {
        Money coin1 = new(1);
        Money coin2 = new(2);
        Money coin3 = new(2);
        Money banknote = new(5);
        (coin1 + coin2 + coin3).Should().Be(banknote);
    }
}
