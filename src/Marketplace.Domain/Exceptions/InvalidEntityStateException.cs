namespace Marketplace.Domain.Exceptions;

public sealed class InvalidEntityStateException : Exception
{
    public InvalidEntityStateException(object entity, string message)
        : base($"Entity {entity.GetType().Name} state change rejected, {message}")
    {
    }
}
