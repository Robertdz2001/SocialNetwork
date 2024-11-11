namespace SocialNetworkBackend.Application.Requests.PostRequests.GetComments;

public class GetCommentsDto
{
    public long UserId { get; set; }

    public string UserFirstName {  get; set; }

    public string UserLastName { get; set; }

    public string Content {  get; set; }

    public DateTime Created { get; set; }

    public bool IsMine {  get; set; }
}