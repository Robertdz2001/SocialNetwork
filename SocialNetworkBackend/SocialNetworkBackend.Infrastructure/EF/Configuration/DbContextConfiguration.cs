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
    IEntityTypeConfiguration<Photo>,
    IEntityTypeConfiguration<FriendInvite>
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
            .WithMany();

        builder
            .HasMany(u => u.SentFriendInvites)
            .WithOne(x => x.Sender)
            .HasForeignKey(x => x.SenderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(u => u.ReceivedFriendInvites)
            .WithOne(x => x.Receiver)
            .HasForeignKey(x => x.ReceiverId)
            .OnDelete(DeleteBehavior.Cascade);
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

    public void Configure(EntityTypeBuilder<FriendInvite> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .HasOne(f => f.Sender)
            .WithMany(u => u.SentFriendInvites)
            .HasForeignKey(f => f.SenderId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(f => f.Receiver)
            .WithMany(u => u.ReceivedFriendInvites)
            .HasForeignKey(f => f.ReceiverId)
            .OnDelete(DeleteBehavior.Cascade);
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