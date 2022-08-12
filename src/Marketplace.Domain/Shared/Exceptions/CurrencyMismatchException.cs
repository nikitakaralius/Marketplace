namespace Marketplace.Domain.Shared.Exceptions;

public sealed class CurrencyMismatchException : Exception
{
    public CurrencyMismatchException(string? message) : base(message)
    {
    }
}
