namespace Marketplace.Projections;

using AdEvent = Domain.ClassifiedAd.Events;
using UserEvent = Domain.UserProfile.Events;
using UpcastEvent = ClassifiedAdUpcastedEvents.V1;

internal sealed class ClassifiedAdDetailsProjection : IProjection
{
    private readonly IList<ReadModels.ClassifiedAdDetails> _items;
    private readonly Func<Guid, string?> _getUserDisplayName;

    public ClassifiedAdDetailsProjection(IList<ReadModels.ClassifiedAdDetails> items,
                                         Func<Guid, string?> getUserDisplayName)
    {
        _items = items;
        _getUserDisplayName = getUserDisplayName;
    }

    public Task ProjectAsync(IEvent @event)
    {
        Action project = @event switch
        {
            AdEvent.ClassifiedAdCreated e => () =>
                _items.Add(new ReadModels.ClassifiedAdDetails
                {
                    Id = e.Id,
                    SellerId = e.OwnerId,
                    SellerDisplayName = _getUserDisplayName(e.OwnerId) ?? ""
                }),
            AdEvent.ClassifiedAdDescriptionUpdated e => () =>
                UpdateItem(e.Id, x => x.Description = e.Description),
            AdEvent.ClassifiedAdPriceUpdated e => () =>
                UpdateItem(e.Id, x => (x.Price, x.CurrencyCode) = (e.Price, e.CurrencyCode)),
            AdEvent.ClassifiedAdTitleChanged e => () =>
                UpdateItem(e.Id, x => x.Title = e.Title),
            UserEvent.UserDisplayNameUpdated e => () =>
                UpdateMultipleItems(
                    q => q.SellerId == e.UserId,
                    ad => ad.SellerDisplayName = e.DisplayName),
            UserEvent.UserRegistered e => () =>
                UpdateMultipleItems(
                    q => q.SellerId == e.UserId,
                    ad => ad.SellerDisplayName = e.DisplayName),
            UpcastEvent.ClassifiedAdPublished e => () =>
                UpdateItem(e.Id, x => x.SellerPhotoUrl = e.SellerPhotoUrl),
            _ => () => { }
        };

        project();

        return Task.CompletedTask;
    }

    private void UpdateItem(Guid id, Action<ReadModels.ClassifiedAdDetails> update)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        if (item is null) return;
        update(item);
    }

    private void UpdateMultipleItems(Func<ReadModels.ClassifiedAdDetails, bool> query,
                                     Action<ReadModels.ClassifiedAdDetails> update)
    {
        foreach (var item in _items.Where(query))
            update(item);
    }
}
