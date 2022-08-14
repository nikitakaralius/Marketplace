using static Marketplace.ClassifiedAds.QueryModels;
using static Marketplace.ClassifiedAds.ReadModels;

namespace Marketplace.ClassifiedAds;

internal interface IClassifiedAdQueryService
{
    Task<IEnumerable<ClassifiedAdListItem>> Query(GetPublishedClassifiedAds query);

    Task<ClassifiedAdDetails> Query(GetPublicClassifiedAd query);

    Task<IEnumerable<ClassifiedAdListItem>> Query(GetOwnersClassifiedAds query);
}
