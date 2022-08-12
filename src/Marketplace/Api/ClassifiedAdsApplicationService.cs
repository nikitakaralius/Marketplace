using Marketplace.Domain.ClassifiedAd;
using Marketplace.Domain.ClassifiedAd.ValueObjects;
using Marketplace.Domain.Shared;
using Marketplace.Domain.UserProfile;
using Marketplace.Framework;
using static Marketplace.Contracts.ClassifiedAds;

namespace Marketplace.Api;

public sealed class ClassifiedAdsApplicationService : IApplicationService<V1.ICommand>
{
    private readonly IClassifiedAdRepository _repository;
    private readonly IUnitOfWork _uow;
    private readonly ICurrencyLookup _currencyLookup;

    public ClassifiedAdsApplicationService(IClassifiedAdRepository repository,
                                           ICurrencyLookup currencyLookup,
                                           IUnitOfWork uow)
    {
        _repository = repository;
        _currencyLookup = currencyLookup;
        _uow = uow;
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

    private async Task<ClassifiedAd> HandleUpdateAsync(Guid id, Action<ClassifiedAd> operation)
    {
        var entity = await LoadClassifiedAd(id);
        operation(entity);
        await _uow.CommitAsync();
        return entity;
    }

    private async Task<ClassifiedAd> CreateAsync(V1.Create request)
    {
        string entityId = request.Id.ToString();
        if (await _repository.ExistsAsync(entityId))
        {
            throw new InvalidOperationException($"Entity with id {entityId} already exists");
        }

        ClassifiedAd entity = new(
            new ClassifiedAdId(request.Id),
            new UserId(request.OwnerId));

        await _repository.AddAsync(entity);
        await _uow.CommitAsync();

        return entity;
    }

    private async Task<ClassifiedAd> SetTitleAsync(V1.SetTitle request) =>
        await HandleUpdateAsync(request.Id, entity =>
        {
            var title = ClassifiedAdTitle.FromString(request.Title);
            entity.SetTitle(title);
        });

    private async Task<ClassifiedAd> UpdateDescriptionAsync(V1.UpdateDescription request) =>
        await HandleUpdateAsync(request.Id, entity =>
        {
            var description = ClassifiedAdDescription.FromString(request.Description);
            entity.UpdateDescription(description);
        });

    private async Task<ClassifiedAd> UpdatePriceAsync(V1.UpdatePrice request) =>
        await HandleUpdateAsync(request.Id, entity =>
        {
            var price = Price.FromDecimal(request.Price, request.Currency, _currencyLookup);
            entity.UpdatePrice(price);
        });

    private async Task<ClassifiedAd> RequestToPublishAsync(V1.RequestToPublish request) =>
        await HandleUpdateAsync(request.Id, entity => { entity.RequestToPublish(); });

    private async Task<ClassifiedAd> LoadClassifiedAd(Guid entityId)
    {
        string id = entityId.ToString();
        var entity = await _repository.LoadAsync(id);
        return entity ?? throw new InvalidOperationException($"Entity with {id} cannot be found");
    }
}
