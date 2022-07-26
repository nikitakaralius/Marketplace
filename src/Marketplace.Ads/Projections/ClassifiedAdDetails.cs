namespace Marketplace.ClassifiedAds.Projections;

public sealed class ClassifiedAdDetails
{
    public Guid Id { get; init; }

    public Guid SellerId { get; init; }

    public string Title { get; set; } = "";

    public string Description { get; set; } = "";

    public decimal Price { get; set; }

    public string CurrencyCode { get; set; } = "";

    public List<string> PhotoUrls { get; set; } = new();
}

