namespace SocialNetworkBackend.Application.Requests.UserRequests.GetUsers;

public class GetUsersDto
{
    public long UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public bool IsFriend { get; set; }

    public bool IsInvited { get; set; }

    public long FriendsCount { get; set; }

    public bool IsBlocked {  get; set; }

    public bool CanBlockUser {  get; set; }
}