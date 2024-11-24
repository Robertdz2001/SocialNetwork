using SocialNetworkBackend.Domain.Entities;

namespace SocialNetworkBackend.Application.Requests.UserRequests.GetUserDetails;

public class GetUserDetailsDto
{
    public long Id { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public bool IsFriend { get; set; }

    public bool IsInvited { get; set; }

    public List<FriendDto> Friends { get; set; } = new();

    public bool IsItMyUser { get; set; }

    public bool IsBlocked {  get; set; }

    public bool CanBlockUser {  get; set; }

    public class FriendDto
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}