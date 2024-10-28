namespace SocialNetworkBackend.Application.Requests.UserRequests.GetMutualFriends;

public class GetMutualFriendsDto
{
    public long UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public bool IsFriend { get; set; }

    public bool IsInvited { get; set; }

    public long FriendsCount { get; set; }

    public long MutualFriendsCount { get; set; }
}