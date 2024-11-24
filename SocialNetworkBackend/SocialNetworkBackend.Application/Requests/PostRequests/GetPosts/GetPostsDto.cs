namespace SocialNetworkBackend.Application.Requests.PostRequests.GetPosts;

public class GetPostsDto
{
    public long CreatedUserId { get; set; }

    public string CreatedUserFirstName { get; set; }

    public string CreatedUserLastName { get; set; }

    public long PostId { get; set; }

    public string Content {  get; set; }

    public DateTime CreatedDate { get; set; }

    public int UserLikesCount {  get; set; }

    public bool IsLiked { get; set; }

    public int CommentsCount {  get; set; }

    public bool CanDelete {  get; set; }
}