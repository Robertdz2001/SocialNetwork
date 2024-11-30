using MediatR;

namespace SocialNetworkBackend.Application.Requests.GroupRequests.AnswerInviteToGroup;

public class AnswerInviteToGroupRequest : IRequest
{
    public long GroupId {  get; set; }

    public bool IsAccepted {  get; set; }
}