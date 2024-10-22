using MediatR;
using SocialNetworkBackend.Application.Pagination;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.UserRequests.GetUsers;

public class GetUsersRequestHandler : IRequestHandler<GetUsersRequest, PagedResult<GetUsersDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserContextService _userContextService;

    public GetUsersRequestHandler(IUserRepository userRepository, IUserContextService userContextService)
    {
        _userRepository = userRepository;
        _userContextService = userContextService;
    }

    public async Task<PagedResult<GetUsersDto>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetUsers(request);

        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var loggedUser = await _userRepository.GetUserById(loggedUserId)
            ?? throw new NotFoundException("User not found");

        users = users.Where(x => x.Id != loggedUserId).ToList();

        var pageSize = 5;

        if (!string.IsNullOrEmpty(request.FirstName))
        {
            users = users.Where(u => u.FirstName.Contains(request.FirstName, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        if (!string.IsNullOrEmpty(request.LastName))
        {
            users = users.Where(u => u.LastName.Contains(request.LastName, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        if (!string.IsNullOrEmpty(request.Country))
        {
            users = users.Where(u => u.Country != null && u.Country.Contains(request.Country, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        if (!string.IsNullOrEmpty(request.City))
        {
            users = users.Where(u => u.City != null && u.City.Contains(request.City, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        var usersDto = users.Select(x => new GetUsersDto
        {
            UserId = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Country = x.Country,
            City = x.City,
            IsFriend = loggedUser.Friends.FirstOrDefault(y =>  y.Id == x.Id) is not null,
            IsInvited = loggedUser.SentFriendInvites.FirstOrDefault(y => y.ReceiverId == x.Id) is not null,
            FriendsCount = x.Friends.Count
        }).ToList();

        var pagedResult = new PagedResult<GetUsersDto>(usersDto, usersDto.Count, pageSize, request.PageNumber);

        return pagedResult;
    }
}