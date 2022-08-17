using Marketplace.EventSourcing;

namespace Marketplace.ClassifiedAds.Projections;

using AdEvent = Domain.ClassifiedAd.Events;
using UserEvent = Domain.UserProfile.Events;

internal sealed class ClassifiedAdDetailsProjection : IProjection
{
    private readonly IList<ClassifiedAdDetails> _items;

    public ClassifiedAdDetailsProjection(IList<ClassifiedAdDetails> items) => _items = items;

    public Task ProjectAsync(IEvent @event)
    {
        Action project = @event switch
        {
            AdEvent.ClassifiedAdCreated e => () =>
                _items.Add(new ClassifiedAdDetails
                {
                    Id = e.Id,
                    SellerId = e.OwnerId
                }),
            AdEvent.ClassifiedAdDescriptionUpdated e => () =>
                UpdateItem(e.Id, x => x.Description = e.Description),
            AdEvent.ClassifiedAdPriceUpdated e => () =>
                UpdateItem(e.Id, x => (x.Price, x.CurrencyCode) = (e.Price, e.CurrencyCode)),
            AdEvent.ClassifiedAdTitleChanged e => () =>
                UpdateItem(e.Id, x => x.Title = e.Title),
            _ => () => { }
        };

        project();

        return Task.CompletedTask;
    }

    private void UpdateItem(Guid id, Action<ClassifiedAdDetails> update)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        if (item is null) return;
        update(item);
    }
}
