using MediatR;

namespace SocialNetworkBackend.Application.Requests.UserRequests.AddFriend;

public class AddFriendRequest : IRequest
{
    public long UserId { get; set; }
}