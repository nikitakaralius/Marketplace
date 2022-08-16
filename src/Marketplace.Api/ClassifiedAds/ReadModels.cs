namespace Marketplace.ClassifiedAds;

public static class ReadModels
{
    public class ClassifiedAdDetails
    {
        public Guid Id { get; init; }

        public Guid SellerId { get; init; }

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public decimal Price { get; set; }

        public string CurrencyCode { get; set; } = "";

        public string SellerDisplayName { get; set; } = "";

        public string[] PhotoUrls { get; set; } = Array.Empty<string>();
    }

    public class ClassifiedAdListItem
    {
        public Guid Id { get; init; }

        public string Title { get; set; } = "";

        public decimal Price { get; set; }

        public string CurrencyCode { get; set; } = "";

        public string PhotoUrl { get; set; } = "";
    }
}
