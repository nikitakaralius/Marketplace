using Marketplace.Domain.ClassifiedAd;
using Marketplace.Domain.UserProfile;
using Marketplace.Infrastructure.EntityFramework.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Infrastructure.EntityFramework;

internal sealed class MarketplaceDbContext : DbContext
{
    private readonly ILoggerFactory _loggerFactory;

    public MarketplaceDbContext(DbContextOptions<MarketplaceDbContext> options, ILoggerFactory loggerFactory) :
        base(options) => _loggerFactory = loggerFactory;

    public DbSet<ClassifiedAd> ClassifiedAds => Set<ClassifiedAd>();

    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseLoggerFactory(_loggerFactory)
                      .EnableSensitiveDataLogging();

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfiguration(new ClassifiedAdEntityTypeConfiguration())
                    .ApplyConfiguration(new PictureEntityTypeConfiguration());
}
