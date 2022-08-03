namespace Marketplace.Framework;

public interface IApplicationService<in TCommand>
{
    public Task HandleAsync(TCommand command);
}
