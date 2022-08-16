namespace Marketplace.Projections;

public static class ReadModels
{
    public class ClassifiedAdDetails
    {
        public Guid Id { get; init; }

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public decimal Price { get; set; }

        public string CurrencyCode { get; set; } = "";

        public string[] PhotoUrls { get; set; } = Array.Empty<string>();

        public Guid SellerId { get; init; }

        public string SellerDisplayName { get; set; } = "";

        public string SellerPhotoUrl { get; set; } = "";
    }

    public class ClassifiedAdListItem
    {
        public Guid Id { get; init; }

        public string Title { get; set; } = "";

        public decimal Price { get; set; }

        public string CurrencyCode { get; set; } = "";

        public string PhotoUrl { get; set; } = "";
    }

    public class UserDetails
    {
        public Guid Id { get; init; }

        public string DisplayName { get; set; }
    }
}
