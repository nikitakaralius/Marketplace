namespace Marketplace.EventSourcing;

public interface IEvent
{

}

public interface IEvent<out T> : IEvent
{

}
