using MediatR;
using SocialNetworkBackend.Application.Pagination;

namespace SocialNetworkBackend.Application.Requests.UserRequests.GetFriendInvites;

public class GetFriendInvitesRequest : IRequest<PagedResult<GetFriendInvitesDto>>
{
    public int PageNumber { get; set; } = 1;
}