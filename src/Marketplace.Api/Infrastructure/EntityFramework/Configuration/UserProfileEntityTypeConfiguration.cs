using Marketplace.Domain.UserProfile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Infrastructure.EntityFramework.Configuration;

internal sealed class UserProfileEntityTypeConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.HasKey(p => p.DatabaseId);

        builder.OwnsOne(p => p.Id)
               .Property(p => p.Value)
               .IsRequired();

        builder.OwnsOne(p => p.FullName)
               .Property(p => p.Value)
               .IsRequired()
               .HasMaxLength(50);

        builder.OwnsOne(p => p.DisplayName)
               .Property(p => p.Value)
               .IsRequired()
               .HasMaxLength(40);
    }
}
