using MediatR;

namespace SocialNetworkBackend.Application.Requests.GroupRequests.InviteToGroup;

public class InviteToGroupRequest : IRequest
{
    public long GroupId { get; set; }

    public long UserId {  get; set; }
}