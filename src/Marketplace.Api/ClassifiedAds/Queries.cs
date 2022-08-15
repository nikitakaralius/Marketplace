namespace Marketplace.ClassifiedAds;

internal static class Queries
{
    public static ReadModels.ClassifiedAdDetails? Query(
        this IEnumerable<ReadModels.ClassifiedAdDetails> items,
        QueryModels.GetPublicClassifiedAd query) =>
        items.FirstOrDefault(x => x.Id == query.ClassifiedAdId);
}
