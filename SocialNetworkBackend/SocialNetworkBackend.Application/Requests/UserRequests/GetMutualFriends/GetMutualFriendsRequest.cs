using MediatR;
using SocialNetworkBackend.Application.Pagination;

namespace SocialNetworkBackend.Application.Requests.UserRequests.GetMutualFriends;

public class GetMutualFriendsRequest : IRequest<PagedResult<GetMutualFriendsDto>>
{
    public int PageNumber { get; set; } = 1;
}