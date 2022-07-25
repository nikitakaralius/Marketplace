namespace Marketplace.Domain.Exceptions;

public sealed class CurrencyMismatchException : Exception
{
    public CurrencyMismatchException(string? message) : base(message)
    {
    }
}
