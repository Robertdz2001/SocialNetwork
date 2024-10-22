using MediatR;

namespace SocialNetworkBackend.Application.Requests.UserRequests.DeleteFriend;

public class DeleteFriendRequest : IRequest
{
    public long UserId { get; set; }
}