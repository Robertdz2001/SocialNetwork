namespace SocialNetworkBackend.Domain.Entities;

public class Message
{
    public long Id { get; set; }

    public string Content { get; set; }

    public DateTime CreatedDate { get; set; }

    public long ChatId { get; set; }

    public Chat Chat { get; set; }

    public long UserId { get; set; }

    public User User { get; set; }
}