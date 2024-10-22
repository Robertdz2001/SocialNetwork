using MediatR;
using SocialNetworkBackend.Application.Pagination;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.UserRequests.GetFriendInvites;

public class GetFriendInvitesRequestHandler : IRequestHandler<GetFriendInvitesRequest, PagedResult<GetFriendInvitesDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserContextService _userContextService;

    public GetFriendInvitesRequestHandler(IUserRepository userRepository, IUserContextService userContextService)
    {
        _userRepository = userRepository;
        _userContextService = userContextService;
    }

    public async Task<PagedResult<GetFriendInvitesDto>> Handle(GetFriendInvitesRequest request, CancellationToken cancellationToken)
    {
        var userId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var user = await _userRepository.GetUserById(userId)
            ?? throw new NotFoundException("User not found");

        var pageSize = 5;

        var friendInvites = user.ReceivedFriendInvites.Select(x => new GetFriendInvitesDto
        {
            UserId = x.SenderId,
            FirstName = x.Sender.FirstName,
            LastName = x.Sender.LastName,
            Country = x.Sender.Country,
            City = x.Sender.City,
            FriendsCount = x.Sender.Friends.Count
        }).ToList();

        return new PagedResult<GetFriendInvitesDto> (friendInvites, friendInvites.Count, pageSize, request.PageNumber );
    }
}
