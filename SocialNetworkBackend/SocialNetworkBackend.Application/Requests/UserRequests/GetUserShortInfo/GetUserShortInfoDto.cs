namespace SocialNetworkBackend.Application.Requests.UserRequests.GetUserShortInfo;

public class GetUserShortInfoDto
{
    public long UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

}