namespace SocialNetworkBackend.Domain.Entities;

public class Photo
{
    public long Id { get; set; }
    public byte[] Data { get; set; }
    public string ContentType { get; set; }

    public long? UserId { get; set; }
    public User? User { get; set; }

    public long? PostId { get; set; }
    public Post? Post { get; set; }
}