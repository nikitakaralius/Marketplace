using Marketplace.Domain.Entities;
using Marketplace.Framework;

namespace Marketplace.Domain;

public sealed class Events
{
    public sealed record ClassifiedAdCreated(Guid Id, Guid OwnerId) : IEvent<ClassifiedAd>;

    public sealed record ClassifiedAdTitleChanged(Guid Id, string Title) : IEvent<ClassifiedAd>;

    public sealed record ClassifiedAdDescriptionUpdated(Guid Id, string Description) : IEvent<ClassifiedAd>;

    public sealed record ClassifiedAdPriceUpdated(Guid Id, decimal Price, string CurrencyCode) : IEvent<ClassifiedAd>;

    public sealed record ClassifiedAdSentToReview(Guid Id) : IEvent<ClassifiedAd>;
}
