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
    IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId);
    }

    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .HasData(GetRoles());
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