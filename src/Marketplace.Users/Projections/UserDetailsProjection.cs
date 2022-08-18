using Marketplace.Domain.UserProfile;
using Marketplace.EventSourcing;

namespace Marketplace.Users.Projections;

public sealed class UserDetailsProjection : IProjection
{
    private readonly IApplyOn<IUserRepository> _repository;

    public UserDetailsProjection(IApplyOn<IUserRepository> repository) => _repository = repository;

    public async Task ProjectAsync(IEvent @event)
    {
        Func<Task> projection = @event switch
        {
            Events.UserRegistered e => () =>
                AddItem(new UserDetails {Id = e.UserId, DisplayName = e.DisplayName}),
            Events.UserDisplayNameUpdated e => () =>
                UpdateItem(e.UserId, x => x.DisplayName = e.DisplayName),
            Events.ProfilePhotoUpdated e => () =>
                UpdateItem(e.UserId, x => x.PhotoUrl = e.PhotoUrl),
            _ => () => Task.CompletedTask
        };
        await projection();
    }

    private async Task AddItem(UserDetails item) =>
        await _repository.ApplyAsync(async r =>
        {
            await r.AddAsync(item);
            await r.SaveChangesAsync();
        });

    private async Task UpdateItem(Guid id, Action<UserDetails> update) =>
        await _repository.ApplyAsync(async r =>
        {
            var item = await r.ByIdAsync(id);
            if (item is null) return;
            update(item);
            await r.SaveChangesAsync();
        });
}
