using Marketplace.Domain.ClassifiedAd;

namespace Marketplace.Projections;

internal static class ClassifiedAdUpcastedEvents
{
    public static class V1
    {
        public class ClassifiedAdPublished : IEvent<ClassifiedAd>
        {
            public Guid Id { get; init; }
            public Guid ApprovedBy { get; init; }
            public Guid OwnerId { get; init; }
            public string SellerPhotoUrl { get; init; } = "";
        }
    }
}
