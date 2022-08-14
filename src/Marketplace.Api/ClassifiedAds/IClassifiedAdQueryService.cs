using static Marketplace.ClassifiedAds.QueryModels;
using static Marketplace.ClassifiedAds.ReadModels;

namespace Marketplace.ClassifiedAds;

internal interface IClassifiedAdQueryService
{
    Task<IEnumerable<ClassifiedAdListItem>> QueryAsync(GetPublishedClassifiedAds query);

    Task<ClassifiedAdDetails> QueryAsync(GetPublicClassifiedAd query);

    Task<IEnumerable<ClassifiedAdListItem>> QueryAsync(GetOwnersClassifiedAds query);
}
