namespace SocialNetworkBackend.Domain.Entities;

public class Post
{
    public long Id { get; set; }

    public long? PhotoId { get; set; }

    public Photo? Photo { get; set; }

    public string Content { get;set; }

    public DateTime Created { get; set; }

    public long CreatedUserId { get; set; }

    public User CreatedUser { get; set; }

    public List<UserLike> UserLikes { get; set; }
}
