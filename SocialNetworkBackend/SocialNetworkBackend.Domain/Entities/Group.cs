namespace SocialNetworkBackend.Domain.Entities;

public class Group
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public long OwnerId { get; set; }

    public User Owner { get; set; }

    public List<Post> Posts { get; set; } = new();

    public List<User> Members { get; set; } = new();

    public List<GroupInvite> GroupInvites { get; set; } = new();

    public long? PhotoId { get; set; }

    public Photo? Photo { get; set; }
}