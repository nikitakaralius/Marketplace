namespace Marketplace.Framework;

public interface IEvent
{

}

public interface IEvent<out T> : IEvent
{

}
