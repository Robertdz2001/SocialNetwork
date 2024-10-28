using MediatR;
using SocialNetworkBackend.Application.Pagination;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.UserRequests.GetMutualFriends;

public class GetMutualFriendsRequestHandler : IRequestHandler<GetMutualFriendsRequest, PagedResult<GetMutualFriendsDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserContextService _userContextService;

    public GetMutualFriendsRequestHandler(IUserRepository userRepository, IUserContextService userContextService)
    {
        _userRepository = userRepository;
        _userContextService = userContextService;
    }

    public async Task<PagedResult<GetMutualFriendsDto>> Handle(GetMutualFriendsRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var mutualFriendsDto = await _userRepository.GetMutualFriends(loggedUserId);

        var pageSize = 5;

        var pagedResult = new PagedResult<GetMutualFriendsDto>(mutualFriendsDto, mutualFriendsDto.Count, pageSize, request.PageNumber);

        return pagedResult;
    }
}