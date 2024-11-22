namespace SocialNetworkBackend.Domain.Entities;

public class Chat
{
    public long Id { get; set; }

    public long User1Id { get; set; }

    public User User1 { get; set; }

    public long User2Id { get; set; }

    public User User2 { get; set; }

    public List<Message> Messages { get; set; } = new();
}