using Marketplace.EntityFramework.Checkpoints;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.EntityFramework.EntityFrameworkCore.Configuration;

internal sealed class CheckpointConfiguration : IEntityTypeConfiguration<Checkpoint>
{
    public void Configure(EntityTypeBuilder<Checkpoint> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
               .HasMaxLength(20)
               .IsRequired();
    }
}
