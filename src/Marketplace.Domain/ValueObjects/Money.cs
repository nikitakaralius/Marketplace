namespace Marketplace.Domain.ValueObjects;

public record Money(decimal Amount)
{
    public Money Add(Money summand) => new(Amount + summand.Amount);

    public Money Subtract(Money subtrahend) => new(Amount - subtrahend.Amount);

    public static Money operator +(Money firstSummand, Money secondSummand) => firstSummand.Add(secondSummand);

    public static Money operator -(Money minuend, Money subtrahend) => minuend.Subtract(subtrahend);
}
