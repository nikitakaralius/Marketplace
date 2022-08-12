using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Infrastructure.EntityFramework.Configuration;

internal sealed class ClassifiedAdEntityTypeConfiguration : IEntityTypeConfiguration<ClassifiedAd>
{
    public void Configure(EntityTypeBuilder<ClassifiedAd> builder)
    {
        builder.HasKey(x => x.DatabaseId);

        builder.OwnsOne(x => x.Id)
               .Property(x => x.Value)
               .IsRequired();

        builder.OwnsOne(x => x.Price, p =>
        {
            p.Property(v => v.Amount).IsRequired();

            p.OwnsOne(c => c.Currency, c =>
            {
                c.Property(x => x.Code).IsRequired();
                c.Property(x => x.DecimalPlaces).IsRequired();
                c.Property(x => x.InUse).IsRequired();
            });
        });

        builder.OwnsOne(x => x.Title)
               .Property(x => x.Value)
               .IsRequired();

        builder.OwnsOne(x => x.Description)
               .Property(x => x.Value)
               .IsRequired();

        builder.OwnsOne(x => x.ApprovedBy)
               .Property(x => x.Value)
               .IsRequired();

        builder.OwnsOne(x => x.OwnerId)
               .Property(x => x.Value)
               .IsRequired();
    }
}
