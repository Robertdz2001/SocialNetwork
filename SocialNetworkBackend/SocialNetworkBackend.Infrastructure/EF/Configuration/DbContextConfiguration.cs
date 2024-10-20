using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetworkBackend.Domain.Entities;
using SocialNetworkBackend.Domain.Enums;
using System.Reflection.Emit;

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
            .HasData(GetUsers());
        builder
            .HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId);
        builder
            .HasOne(u => u.Photo)
            .WithOne()
            .HasForeignKey<User>(u => u.PhotoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(u => u.Friends)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "UserFriend",
                j => j.HasOne<User>().WithMany().HasForeignKey("FriendId").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade)
            );

        builder
            .HasMany(u => u.FriendInvites)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "UserInvite",
                j => j.HasOne<User>().WithMany().HasForeignKey("ReceiverId").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<User>().WithMany().HasForeignKey("SenderId").OnDelete(DeleteBehavior.Cascade)
            );
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

    private IEnumerable<User> GetUsers()
    {

        var users = new List<User>();

        for (int i = 1; i <= 30; i++)
        {
            users.Add(new User
            {
                Id = i,
                Email = $"user{i}@gmail.com",
                FirstName = $"FirstName{i}",
                LastName = $"LastName{i}",
                PhoneNumber = $"+{i}543654753",
                Country = $"Country{i}",
                City = $"City{i}",
                RoleId = (long)UserRoles.User,
                PasswordHash = "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==" //Test!23456
            });
        }

        return users;
    }
}