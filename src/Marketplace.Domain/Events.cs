using Marketplace.Domain.Entities;
using Marketplace.Framework;

namespace Marketplace.Domain;

using ClassifiedAdEvent = IEvent<ClassifiedAd>;
using PictureEvent = IEvent<Picture>;

public static class Events
{
    public sealed record ClassifiedAdCreated(Guid Id, Guid OwnerId) : ClassifiedAdEvent;

    public sealed record ClassifiedAdTitleChanged(Guid Id, string Title) : ClassifiedAdEvent;

    public sealed record ClassifiedAdDescriptionUpdated(Guid Id, string Description) : ClassifiedAdEvent;

    public sealed record ClassifiedAdPriceUpdated(Guid Id, decimal Price, string CurrencyCode) : ClassifiedAdEvent;

    public sealed record ClassifiedAdSentForReview(Guid Id) : ClassifiedAdEvent;

    public sealed record PictureAddedToClassifiedAd(
        Guid ClassifiedAdId,
        Guid PictureId,
        string Url,
        int Height,
        int Width,
        int Order) : ClassifiedAdEvent;
}
