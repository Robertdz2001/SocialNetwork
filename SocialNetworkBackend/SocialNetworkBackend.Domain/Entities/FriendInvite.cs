namespace SocialNetworkBackend.Domain.Entities;

public class FriendInvite
{
    public long Id { get; set; }

    public long ReceiverId { get; set; }

    public User Receiver { get; set; }  

    public long SenderId { get; set; }

    public User Sender { get; set; }
}