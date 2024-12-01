using MediatR;
using SocialNetworkBackend.Application.Pagination;

namespace SocialNetworkBackend.Application.Requests.GroupRequests.GetGroupInvites;

public class GetGroupInvitesRequest : IRequest<PagedResult<GetGroupInvitesDto>>
{
    public int PageNumber { get; set; } = 1;
}