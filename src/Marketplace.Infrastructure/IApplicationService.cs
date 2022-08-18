namespace Marketplace.Infrastructure;

public interface IApplicationService<in TCommand>
{
    public Task HandleAsync(TCommand command);
}
