using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetworkBackend.Domain.Entities;
using SocialNetworkBackend.Domain.Enums;

namespace SocialNetworkBackend.Infrastructure.EF.Configuration;

/// <summary>
/// Configurations for DbContext.
/// </summary>
public class DbContextConfiguration :
    IEntityTypeConfiguration<User>,
    IEntityTypeConfiguration<Role>,
    IEntityTypeConfiguration<VerificationToken>,
    IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId);
        builder            
            .HasOne(u => u.Photo)
            .WithOne()
            .HasForeignKey<User>(u => u.PhotoId);
    }

    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .HasData(GetRoles());
    }

    public void Configure(EntityTypeBuilder<VerificationToken> builder)
    {
        builder
            .HasKey(x => x.Id);
    }

    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .HasOne(x => x.User)
            .WithOne(x => x.Photo)
            .HasForeignKey<Photo>(x => x.UserId);
    }

    private IEnumerable<Role> GetRoles()
    {

        var roles = new List<Role>
        {
            new()
            {
                Id = (long)UserRoles.User,
                Name = "User"

            },
            new()
            {
                Id = (long)UserRoles.Admin,
                Name = "Admin"
            }
        };

        return roles;
    }
}