using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Infrastructure.EntityFramework.Configuration;

internal sealed class ClassifiedAdEntityTypeConfiguration : IEntityTypeConfiguration<ClassifiedAd>
{
    public void Configure(EntityTypeBuilder<ClassifiedAd> builder) => builder.HasKey(x => x.Id);
}
