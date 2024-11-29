using MediatR;
using SocialNetworkBackend.Application.Pagination;

namespace SocialNetworkBackend.Application.Requests.GroupRequests.GetGroups;

public class GetGroupsRequest : IRequest<PagedResult<GetGroupsDto>>
{
    public bool ShowOnlyWhereIsMember {  get; set; }

    public bool ShowOnlyWhereIsOwner {  get; set; }

    public string? Name {  get; set; }

    public int PageNumber { get; set; } = 1;
}