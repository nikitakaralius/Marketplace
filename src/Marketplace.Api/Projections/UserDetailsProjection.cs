using Marketplace.Domain.UserProfile;

namespace Marketplace.Projections;

internal sealed class UserDetailsProjection : IProjection
{
    private readonly IList<ReadModels.UserDetails> _items;

    public UserDetailsProjection(IList<ReadModels.UserDetails> items) => _items = items;

    public Task ProjectAsync(IEvent @event)
    {
        Action projection = @event switch
        {
            Events.UserRegistered e => () =>
                _items.Add(new ReadModels.UserDetails {Id = e.UserId, DisplayName = e.DisplayName}),
            Events.UserDisplayNameUpdated e => () =>
                UpdateItem(e.UserId, x => x.DisplayName = e.DisplayName),
            Events.ProfilePhotoUpdated e => () =>
                UpdateItem(e.UserId, x => x.PhotoUrl = e.PhotoUrl),
            _ => () => { }
        };
        projection();
        return Task.CompletedTask;
    }

    private void UpdateItem(Guid id, Action<ReadModels.UserDetails> update)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        if (item is null) return;
        update(item);
    }
}
