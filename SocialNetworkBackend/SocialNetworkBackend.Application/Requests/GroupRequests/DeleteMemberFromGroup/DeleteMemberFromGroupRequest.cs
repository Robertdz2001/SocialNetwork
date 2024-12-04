using MediatR;

namespace SocialNetworkBackend.Application.Requests.GroupRequests.DeleteMemberFromGroup;

public class DeleteMemberFromGroupRequest : IRequest
{
    public long GroupId { get; set; }

    public long MemberId { get; set; }
}