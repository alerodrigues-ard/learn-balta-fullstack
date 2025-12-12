using Dima.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Identity;

public class IdentityUserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("IdentityUser");    // Originalmente seria AspNetUsers
        builder.HasKey(u => u.Id);
        
        builder.HasIndex(u => u.NormalizedUserName).IsUnique();
        builder.HasIndex(u => u.NormalizedEmail).IsUnique();

        builder.Property(u => u.Email).HasMaxLength(180);
        builder.Property(u => u.NormalizedEmail).HasMaxLength(180);
        builder.Property(u => u.UserName).HasMaxLength(180);
        builder.Property(u => u.NormalizedUserName).HasMaxLength(180);
        builder.Property(u => u.PhoneNumber).HasMaxLength(20);
        builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
        
        builder.HasMany<IdentityUserClaim<uint>>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
        builder.HasMany<IdentityUserLogin<uint>>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
        builder.HasMany<IdentityUserToken<uint>>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
        builder.HasMany<IdentityUserRole<uint>>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
    }
}