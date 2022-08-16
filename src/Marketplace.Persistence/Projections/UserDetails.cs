namespace Marketplace.Persistence.Projections;

public sealed class UserDetails
{
    public Guid Id { get; init; }

    public string DisplayName { get; set; } = "";

    public string? PhotoUrl { get; set; } = "";
}
