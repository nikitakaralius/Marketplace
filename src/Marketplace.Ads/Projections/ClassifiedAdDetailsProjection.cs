using Marketplace.EventSourcing;

namespace Marketplace.ClassifiedAds.Projections;

using AdEvent = Domain.ClassifiedAd.Events;

public sealed class ClassifiedAdDetailsProjection : IProjection
{
    private readonly IApplyOn<IClassifiedAdRepository> _repository;

    public ClassifiedAdDetailsProjection(IApplyOn<IClassifiedAdRepository> repository) =>
        _repository = repository;

    public async Task ProjectAsync(IEvent @event)
    {
        Func<Task> project = @event switch
        {
            AdEvent.ClassifiedAdCreated e => () =>
                AddItem(new ClassifiedAdDetails
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
    }

    private async Task AddItem(ClassifiedAdDetails item) =>
        await _repository.ApplyAsync(async r =>
        {
            await r.AddAsync(item);
            await r.SaveChangesAsync();
        });

    private async Task UpdateItem(Guid id, Action<ClassifiedAdDetails> update) =>
        await _repository.ApplyAsync(async r =>
        {
            var item = await r.ByIdAsync(id);
            if (item is null) return;
            update(item);
            await r.SaveChangesAsync();
        });
}
