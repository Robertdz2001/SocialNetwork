using MediatR;
using SocialNetworkBackend.Application.Pagination;

namespace SocialNetworkBackend.Application.Requests.PostRequests.GetPosts;

public class GetPostsRequest : IRequest<PagedResult<GetPostsDto>>
{
    public string? CreatedUserFirstName { get; set; }

    public string? CreatedUserLastName { get; set; }

    public string? Content {  get; set; }

    public int PageNumber { get; set; } = 1;
}