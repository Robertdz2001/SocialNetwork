namespace SocialNetworkBackend.Application.Requests.UserRequests.GetFriendInvites;

public class GetFriendInvitesDto
{
    public long UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }
}