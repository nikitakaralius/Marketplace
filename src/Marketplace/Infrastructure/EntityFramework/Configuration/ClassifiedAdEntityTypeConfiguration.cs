using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Infrastructure.EntityFramework.Configuration;

internal sealed class ClassifiedAdEntityTypeConfiguration : IEntityTypeConfiguration<ClassifiedAd>
{
    public void Configure(EntityTypeBuilder<ClassifiedAd> builder)
    {
        builder.HasKey(x => x.DatabaseId);
        builder.OwnsOne(x => x.Id);
        builder.OwnsOne(x => x.Price, p => p.OwnsOne(c => c.Currency));
        builder.OwnsOne(x => x.Title);
        builder.OwnsOne(x => x.Description);
        builder.OwnsOne(x => x.ApprovedBy);
        builder.OwnsOne(x => x.OwnerId);
    }
}
