using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.EntityFramework.Configuration;

internal sealed class UserDetailsConfiguration : IEntityTypeConfiguration<UserDetails>
{
    public void Configure(EntityTypeBuilder<UserDetails> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.DisplayName)
               .HasMaxLength(40)
               .IsRequired();
    }
}
