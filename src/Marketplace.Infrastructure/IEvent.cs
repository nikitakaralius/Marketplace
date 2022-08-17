namespace Marketplace.Infrastructure;

public interface IEvent
{

}

public interface IEvent<out T> : IEvent
{

}
