using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.EntityFramework.Configuration;

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

        // builder.Property(x => x.PhotoUrls)
        //        .HasConversion(
        //            v => string.Join(',', v),
        //            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
        //                  .ToList());
    }
}
