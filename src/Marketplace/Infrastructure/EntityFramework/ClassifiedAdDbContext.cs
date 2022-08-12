using Marketplace.Infrastructure.EntityFramework.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Infrastructure.EntityFramework;

internal sealed class ClassifiedAdDbContext : DbContext
{
    private readonly ILoggerFactory _loggerFactory;

    public ClassifiedAdDbContext(DbContextOptions<ClassifiedAdDbContext> options, ILoggerFactory loggerFactory) :
        base(options) => _loggerFactory = loggerFactory;

    public DbSet<ClassifiedAd> ClassifiedAds => Set<ClassifiedAd>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseLoggerFactory(_loggerFactory)
                      .EnableSensitiveDataLogging();

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfiguration(new ClassifiedAdEntityTypeConfiguration())
                    .ApplyConfiguration(new PictureEntityTypeConfiguration());
}
