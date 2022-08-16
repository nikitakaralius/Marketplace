using Marketplace.Persistence.EntityFrameworkCore.Configuration;

namespace Marketplace.Persistence.EntityFrameworkCore;

internal sealed class MarketplaceDbContext : DbContext
{
    public MarketplaceDbContext(DbContextOptions<MarketplaceDbContext> options) : base(options)
    {
    }

    public DbSet<Checkpoint> Checkpoints => Set<Checkpoint>();

    public DbSet<ClassifiedAdDetails> ClassifiedAds => Set<ClassifiedAdDetails>();

    public DbSet<UserDetails> Users => Set<UserDetails>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfiguration(new CheckpointConfiguration())
                    .ApplyConfiguration(new ClassifiedAdDetailsConfiguration())
                    .ApplyConfiguration(new UserDetailsConfiguration());
}
