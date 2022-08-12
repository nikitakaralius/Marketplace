namespace Marketplace.Domain.Shared;

public static class DomainException
{
    public sealed class InvalidEntityState : Exception
    {
        public InvalidEntityState(object entity, string message)
            : base($"Entity {entity.GetType().Name} state change rejected, {message}")
        {
        }
    }

    public sealed class CurrencyMismatch : Exception
    {
        public CurrencyMismatch(string? message) : base(message)
        {
        }
    }

    public sealed class ProfanityFound : Exception
    {
        public ProfanityFound(string text) : base($"Profanity found in text: {text}") { }
    }
}
