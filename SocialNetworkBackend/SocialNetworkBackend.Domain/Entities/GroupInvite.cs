namespace SocialNetworkBackend.Domain.Entities;

public class GroupInvite
{
    public long Id { get; set; }

    public long ReceiverId { get; set; }

    public User Receiver { get; set; }

    public long GroupId { get; set; }

    public Group Group { get; set; }
}