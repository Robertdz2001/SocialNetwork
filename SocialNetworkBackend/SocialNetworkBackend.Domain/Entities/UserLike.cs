namespace SocialNetworkBackend.Domain.Entities;

public class UserLike
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public User User { get; set; }

    public long PostId {  get; set; }

    public Post Post { get; set; }
}