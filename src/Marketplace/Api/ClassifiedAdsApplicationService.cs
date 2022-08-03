using Marketplace.Domain.Entities;
using Marketplace.Domain.Services;
using Marketplace.Domain.ValueObjects;
using Marketplace.Framework;
using static Marketplace.Contracts.ClassifiedAds;

namespace Marketplace.Api;

public sealed class ClassifiedAdsApplicationService : IApplicationService<V1.ICommand>
{
    private readonly IEntityStore _entityStore;
    private readonly ICurrencyLookup _currencyLookup;

    public ClassifiedAdsApplicationService(IEntityStore entityStore, ICurrencyLookup currencyLookup)
    {
        _entityStore = entityStore;
        _currencyLookup = currencyLookup;
    }

    public async Task HandleAsync(V1.ICommand command)
    {
        var _ = command switch
        {
            V1.Create r            => await CreateAsync(r),
            V1.SetTitle r          => await SetTitleAsync(r),
            V1.UpdateDescription r => await UpdateDescriptionAsync(r),
            V1.UpdatePrice r       => await UpdatePriceAsync(r),
            V1.RequestToPublish r  => await RequestToPublishAsync(r),
            _ =>
                throw new InvalidOperationException($"Command type {command.GetType().Name} is unknown")
        };
    }

    private async Task<ClassifiedAd> CreateAsync(V1.Create request)
    {
        string entityId = request.Id.ToString();
        if (await _entityStore.ExistsAsync<ClassifiedAd>(entityId))
        {
            throw new InvalidOperationException($"Entity with id {entityId} already exists");
        }

        ClassifiedAd entity = new(
            new ClassifiedAdId(request.Id),
            new UserId(request.OwnerId));

        await _entityStore.SaveAsync(entity);

        return entity;
    }

    private async Task<ClassifiedAd> SetTitleAsync(V1.SetTitle request)
    {
        var entity = await LoadClassifiedAd(request.Id);
        var title = ClassifiedAdTitle.FromString(request.Title);
        entity.SetTitle(title);
        await _entityStore.SaveAsync(entity);
        return entity;
    }

    private async Task<ClassifiedAd> UpdateDescriptionAsync(V1.UpdateDescription request)
    {
        var entity = await LoadClassifiedAd(request.Id);
        var description = ClassifiedAdDescription.FromString(request.Description);
        entity.UpdateDescription(description);
        await _entityStore.SaveAsync(entity);
        return entity;
    }

    private async Task<ClassifiedAd> UpdatePriceAsync(V1.UpdatePrice request)
    {
        var entity = await LoadClassifiedAd(request.Id);
        var price = Price.FromDecimal(request.Price, request.Currency, _currencyLookup);
        entity.UpdatePrice(price);
        await _entityStore.SaveAsync(entity);
        return entity;
    }

    private async Task<ClassifiedAd> RequestToPublishAsync(V1.RequestToPublish request)
    {
        var entity = await LoadClassifiedAd(request.Id);
        entity.RequestToPublish();
        await _entityStore.SaveAsync(entity);
        return entity;
    }

    private async Task<ClassifiedAd> LoadClassifiedAd(Guid entityId)
    {
        string id = entityId.ToString();
        var entity = await _entityStore.LoadAsync<ClassifiedAd>(id);
        return entity ?? throw new InvalidOperationException($"Entity with {id} cannot be found");
    }
}
