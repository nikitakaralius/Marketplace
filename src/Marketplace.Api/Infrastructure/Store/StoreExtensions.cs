using Marketplace.Domain.ClassifiedAd;
using Marketplace.Domain.ClassifiedAd.ValueObjects;
using Marketplace.Domain.UserProfile;
using Marketplace.Domain.UserProfile.ValueObjects;
using Marketplace.Infrastructure.Persistence;

namespace Marketplace.Infrastructure.Store;

internal static class StoreExtensions
{
    public static Task<bool> ExistsAsync(this IAggregateStore store, ClassifiedAdId id) =>
        store.ExistsAsync<ClassifiedAd, ClassifiedAdId>(id);

    public static Task SaveAsync(this IAggregateStore store, ClassifiedAd classifiedAd) =>
        store.SaveAsync<ClassifiedAd, ClassifiedAdId>(classifiedAd);

    public static Task<ClassifiedAd> LoadAsync(this IAggregateStore store, ClassifiedAdId id) =>
        store.LoadAsync<ClassifiedAd, ClassifiedAdId>(id);

    public static Task<bool> ExistsAsync(this IAggregateStore store, UserId id) =>
        store.ExistsAsync<UserProfile, UserId>(id);

    public static Task SaveAsync(this IAggregateStore store, UserProfile profile) =>
        store.SaveAsync<UserProfile, UserId>(profile);

    public static Task<UserProfile> LoadAsync(this IAggregateStore store, UserId id) =>
        store.LoadAsync<UserProfile, UserId>(id);
}
