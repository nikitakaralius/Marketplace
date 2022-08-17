using Marketplace.EventSourcing;

namespace Marketplace.ClassifiedAds.Projections;

using AdEvent = Domain.ClassifiedAd.Events;

internal sealed class ClassifiedAdDetailsProjection : IProjection
{
    private readonly IClassifiedAdRepository _repository;

    public ClassifiedAdDetailsProjection(IClassifiedAdRepository repository) =>
        _repository = repository;

    public async Task ProjectAsync(IEvent @event)
    {
        Func<Task> project = @event switch
        {
            AdEvent.ClassifiedAdCreated e => () =>
                _repository.AddAsync(new ClassifiedAdDetails
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
            _ => () => Task.CompletedTask
        };

        await project();
        await _repository.SaveChangesAsync();
    }

    private async Task UpdateItem(Guid id, Action<ClassifiedAdDetails> update)
    {
        var item = await _repository.ByIdAsync(id);
        if (item is null) return;
        update(item);
    }
}
