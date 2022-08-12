using Marketplace.Domain.ClassifiedAd;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Infrastructure.EntityFramework.Configuration;

internal sealed class PictureEntityTypeConfiguration : IEntityTypeConfiguration<Picture>
{
    public void Configure(EntityTypeBuilder<Picture> builder)
    {
        builder.HasKey(x => x.DatabaseId);
        builder.OwnsOne(x => x.Id);
        builder.OwnsOne(x => x.ParentId);
        builder.OwnsOne(x => x.Size);
    }
}
