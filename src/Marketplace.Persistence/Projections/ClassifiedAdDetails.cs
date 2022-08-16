namespace Marketplace.Persistence.Projections;

public sealed class ClassifiedAdDetails
{
    public Guid Id { get; init; }

    public string Title { get; set; } = "";

    public string Description { get; set; } = "";

    public decimal Price { get; set; }

    public string CurrencyCode { get; set; } = "";

    public string[] PhotoUrls { get; set; } = Array.Empty<string>();

    public UserDetails Seller { get; init; } = new();
}
