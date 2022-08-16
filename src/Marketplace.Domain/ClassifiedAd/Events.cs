using Marketplace.Framework;

namespace Marketplace.Domain.ClassifiedAd;

using ClassifiedAdEvent = IEvent<Domain.ClassifiedAd.ClassifiedAd>;
using PictureEvent = IEvent<Picture>;

public static class Events
{
    public sealed class ClassifiedAdCreated : IEvent<ClassifiedAd>
    {
        public Guid Id { get; init; }
        public Guid OwnerId { get; init; }
    }

    public sealed class ClassifiedAdTitleChanged : IEvent<ClassifiedAd>
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = null!;
    }

    public sealed class ClassifiedAdDescriptionUpdated : IEvent<ClassifiedAd>
    {
        public Guid Id { get; init; }
        public string Description { get; init; } = null!;
    }

    public sealed class ClassifiedAdPriceUpdated : IEvent<ClassifiedAd>
    {
        public Guid Id { get; init; }
        public decimal Price { get; init; }
        public string CurrencyCode { get; init; } = null!;
    }

    public sealed class ClassifiedAdSentForReview : IEvent<ClassifiedAd>
    {
        public Guid Id { get; init; }
    }

    public class ClassifiedAdPublished : IEvent<ClassifiedAd>
    {
        public Guid Id { get; init; }
        public Guid ApprovedBy { get; init; }
        public Guid OwnerId { get; init; }
    }

    public sealed class PictureAddedToClassifiedAd : IEvent<ClassifiedAd>, PictureEvent
    {
        public Guid ClassifiedAdId { get; init; }
        public Guid PictureId { get; init; }
        public string Url { get; init; } = null!;
        public int Height { get; init; }
        public int Width { get; init; }
        public int Order { get; init; }
    }

    public sealed class PictureResized : PictureEvent
    {
        public Guid PictureId { get; init; }
        public int Height { get; init; }
        public int Width { get; init; }
    }
}
