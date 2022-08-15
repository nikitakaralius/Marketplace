namespace Marketplace.ClassifiedAds;

internal static class ReadModels
{
    public class ClassifiedAdDetails
    {
        public Guid Id { get; init; }

        public string Title { get; init; } = "";

        public string Description { get; init; } = "";

        public decimal Price { get; init; }

        public string CurrencyCode { get; init; } = "";

        public string SellerDisplayName { get; init; } = "";

        public string[] PhotoUrls { get; init; } = Array.Empty<string>();
    }

    public class ClassifiedAdListItem
    {
        public Guid Id { get; init; }

        public string Title { get; init; } = "";

        public decimal Price { get; init; }

        public string CurrencyCode { get; init; } = "";

        public string PhotoUrl { get; init; } = "";
    }
}
