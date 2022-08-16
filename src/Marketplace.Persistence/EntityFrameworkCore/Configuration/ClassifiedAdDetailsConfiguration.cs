using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Persistence.EntityFrameworkCore.Configuration;

internal sealed class ClassifiedAdDetailsConfiguration : IEntityTypeConfiguration<ClassifiedAdDetails>
{
    public void Configure(EntityTypeBuilder<ClassifiedAdDetails> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(x => x.Description)
               .HasMaxLength(1500)
               .IsRequired();

        builder.Property(x => x.PhotoUrls)
               .HasConversion(
                   v => string.Join(',', v),
                   v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

        builder.OwnsOne(x => x.Seller, s =>
        {
            s.Property(p => p.Id);
            s.Property(p => p.DisplayName);
            s.Property(p => p.PhotoUrl);
        });
    }
}