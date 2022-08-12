using Marketplace.Framework;

namespace Marketplace.Domain.ClassifiedAd;

using ClassifiedAdEvent = IEvent<Domain.ClassifiedAd.ClassifiedAd>;
using PictureEvent = IEvent<Picture>;

public static class Events
{
    public sealed record ClassifiedAdCreated(Guid Id, Guid OwnerId) : IEvent<ClassifiedAd>;

    public sealed record ClassifiedAdTitleChanged(Guid Id, string Title) : IEvent<ClassifiedAd>;

    public sealed record ClassifiedAdDescriptionUpdated(Guid Id, string Description) : IEvent<ClassifiedAd>;

    public sealed record ClassifiedAdPriceUpdated(Guid Id, decimal Price, string CurrencyCode) : IEvent<ClassifiedAd>;

    public sealed record ClassifiedAdSentForReview(Guid Id) : IEvent<ClassifiedAd>;

    public sealed record PictureAddedToClassifiedAd(
        Guid ClassifiedAdId,
        Guid PictureId,
        string Url,
        int Height,
        int Width,
        int Order) : IEvent<ClassifiedAd>, PictureEvent;

    public sealed record PictureResized(Guid PictureId, int Height, int Width) : PictureEvent;
}
