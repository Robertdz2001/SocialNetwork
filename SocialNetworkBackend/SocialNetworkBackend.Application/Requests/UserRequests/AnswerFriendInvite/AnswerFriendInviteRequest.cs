using MediatR;

namespace SocialNetworkBackend.Application.Requests.UserRequests.AnswerFriendInvite;

public class AnswerFriendInviteRequest : IRequest
{
    public long UserId { get; set; }
    public bool IsAccepted { get; set; }
}
