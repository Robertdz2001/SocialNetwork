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
    IEntityTypeConfiguration<Photo>,
    IEntityTypeConfiguration<FriendInvite>,
    IEntityTypeConfiguration<Post>,
    IEntityTypeConfiguration<UserLike>,
    IEntityTypeConfiguration<UserComment>,
    IEntityTypeConfiguration<Chat>,
    IEntityTypeConfiguration<Message>,
    IEntityTypeConfiguration<Group>,
    IEntityTypeConfiguration<GroupInvite>
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
        builder
            .HasOne(x => x.Post)
            .WithOne(x => x.Photo)
            .HasForeignKey<Photo>(x => x.PostId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(x => x.Group)
            .WithOne(x => x.Photo)
            .HasForeignKey<Photo>(x => x.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
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

    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .HasOne(x => x.CreatedUser)
            .WithMany(y => y.Posts)
            .HasForeignKey(x => x.CreatedUserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(u => u.Photo)
            .WithOne()
            .HasForeignKey<Post>(u => u.PhotoId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(x => x.UserLikes)
            .WithOne(y => y.Post);
    }

    public void Configure(EntityTypeBuilder<UserLike> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(x => x.Post)
            .WithMany(y => y.UserLikes)
            .HasForeignKey(x => x.PostId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public void Configure(EntityTypeBuilder<UserComment> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(x => x.Post)
            .WithMany(y => y.UserComments)
            .HasForeignKey(x => x.PostId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder
            .HasOne(c => c.User1)
            .WithMany(u => u.ChatsAsUser1)
            .HasForeignKey(c => c.User1Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(c => c.User2)
            .WithMany(u => u.ChatsAsUser2)
            .HasForeignKey(c => c.User2Id)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder
            .HasOne(m => m.Chat)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.ChatId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(m => m.User)
            .WithMany()
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasOne(u => u.Owner)
            .WithMany()
            .HasForeignKey(u => u.OwnerId);

        builder
            .HasOne(u => u.Photo)
            .WithOne(x => x.Group)
            .HasForeignKey<Group>(u => u.PhotoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(u => u.Members)
            .WithMany(x => x.Groups);

        builder
            .HasMany(u => u.GroupInvites)
            .WithOne(x => x.Group)
            .HasForeignKey(x => x.GroupId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(u => u.Posts)
            .WithOne(x => x.Group)
            .HasForeignKey(x => x.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public void Configure(EntityTypeBuilder<GroupInvite> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .HasOne(x => x.Group)
            .WithMany(x => x.GroupInvites)
            .HasForeignKey(x => x.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(x => x.Receiver)
            .WithMany(x => x.GroupInvites)
            .HasForeignKey(x => x.ReceiverId)
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