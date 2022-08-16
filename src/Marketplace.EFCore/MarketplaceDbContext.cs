using Marketplace.EFCore.Entities;

namespace Marketplace.EFCore;

public sealed class MarketplaceDbContext : DbContext
{
    public MarketplaceDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Checkpoint> Checkpoints => Set<Checkpoint>();
}
