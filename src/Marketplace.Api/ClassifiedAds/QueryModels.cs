namespace Marketplace.ClassifiedAds;

internal static class QueryModels
{
    public class GetPublishedClassifiedAds
    {
        public int Page { get; init; }

        public int PageSize { get; init; }
    }

    public class GetOwnersClassifiedAds
    {
        public Guid OwnerId { get; init; }

        public int Page { get; init; }

        public int PageSize { get; init; }
    }

    public class GetPublicClassifiedAd
    {
        public Guid ClassifiedAdId { get; init; }
    }
}
