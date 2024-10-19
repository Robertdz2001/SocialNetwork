using MediatR;
using SocialNetworkBackend.Application.Pagination;
using SocialNetworkBackend.Application.Repositories;

namespace SocialNetworkBackend.Application.Requests.UserRequests.GetUsers;

public class GetUsersRequestHandler : IRequestHandler<GetUsersRequest, PagedResult<GetUsersDto>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersRequestHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<PagedResult<GetUsersDto>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetUsers(request);

        var pageSize = 1;

        if (!string.IsNullOrEmpty(request.FirstName))
        {
            users = users.Where(u => u.FirstName.Contains(request.FirstName)).ToList();
        }

        if (!string.IsNullOrEmpty(request.LastName))
        {
            users = users.Where(u => u.LastName.Contains(request.LastName)).ToList();
        }

        if (!string.IsNullOrEmpty(request.Country))
        {
            users = users.Where(u => u.Country != null && u.Country.Contains(request.Country)).ToList();
        }

        if (!string.IsNullOrEmpty(request.City))
        {
            users = users.Where(u => u.City != null && u.City.Contains(request.City)).ToList();
        }

        var usersDto = users.Select(x => new GetUsersDto
        {
            UserId = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Country = x.Country,
            City = x.City
        }).ToList();

        var pagedResult = new PagedResult<GetUsersDto>(usersDto, usersDto.Count(), 1, request.PageNumber);

        return pagedResult;
    }
}