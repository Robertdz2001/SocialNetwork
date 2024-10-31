namespace SocialNetworkBackend.Domain.Entities;

public class User
{
    public long Id { get; set; }

    public required string Email { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public Role Role { get; set; }

    public long RoleId { get; set; }

    public string PasswordHash { get; set; }

    public long? PhotoId { get; set; }

    public Photo? Photo { get; set; }

    public List<User> Friends { get; set; } = new();

    public List<FriendInvite> SentFriendInvites { get; set; } = new();

    public List<FriendInvite> ReceivedFriendInvites { get; set; } = new();

    public List<Post> Posts { get; set; } = new();
}