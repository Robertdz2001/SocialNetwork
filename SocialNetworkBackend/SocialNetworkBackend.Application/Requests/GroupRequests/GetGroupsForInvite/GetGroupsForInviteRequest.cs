using MediatR;

namespace SocialNetworkBackend.Application.Requests.GroupRequests.GetGroupsForInvite;

public class GetGroupsForInviteRequest : IRequest<List<GetGroupsForInviteDto>>
{
    public long UserId { get; set; }
}