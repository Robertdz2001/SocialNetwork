using MediatR;
using SocialNetworkBackend.Application.Pagination;

namespace SocialNetworkBackend.Application.Requests.UserRequests.GetUsers;

public class GetUsersRequest : IRequest<PagedResult<GetUsersDto>>
{
    public string? FirstName {  get; set; }

    public string? LastName { get; set; }

    public string? Country { get; set; }

    public string? City {  get; set; }

    public int PageNumber { get; set; } = 1;
}