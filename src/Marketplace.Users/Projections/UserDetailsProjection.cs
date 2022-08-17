using Marketplace.Domain.UserProfile;
using Marketplace.EventSourcing;

namespace Marketplace.Users.Projections;

internal sealed class UserDetailsProjection : IProjection
{
    private readonly IUserRepository _repository;

    public UserDetailsProjection(IUserRepository repository) => _repository = repository;

    public async Task ProjectAsync(IEvent @event)
    {
        Func<Task> projection = @event switch
        {
            Events.UserRegistered e => () =>
                _repository.AddAsync(new UserDetails {Id = e.UserId, DisplayName = e.DisplayName}),
            Events.UserDisplayNameUpdated e => () =>
                UpdateItem(e.UserId, x => x.DisplayName = e.DisplayName),
            Events.ProfilePhotoUpdated e => () =>
                UpdateItem(e.UserId, x => x.PhotoUrl = e.PhotoUrl),
            _ => () => Task.CompletedTask
        };
        await projection();
        await _repository.SaveChangesAsync();
    }

    private async Task UpdateItem(Guid id, Action<UserDetails> update)
    {
        var item = await _repository.ByIdAsync(id);
        if (item is null) return;
        update(item);
    }
}
