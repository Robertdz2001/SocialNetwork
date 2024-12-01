using MediatR;
using SocialNetworkBackend.Application.Pagination;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.GroupRequests.GetGroupInvites;

public class GetGroupInvitesRequestHandler : IRequestHandler<GetGroupInvitesRequest, PagedResult<GetGroupInvitesDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserContextService _userContextService;

    public GetGroupInvitesRequestHandler(IUserRepository userRepository, IUserContextService userContextService)
    {
        _userRepository = userRepository;
        _userContextService = userContextService;
    }

    public async Task<PagedResult<GetGroupInvitesDto>> Handle(GetGroupInvitesRequest request, CancellationToken cancellationToken)
    {
        var userId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var user = await _userRepository.GetUserById(userId)
            ?? throw new NotFoundException("User not found");

        var pageSize = 5;

        var groupInvites = user.GroupInvites.Select(x => new GetGroupInvitesDto
        {
            Id = x.GroupId,
            Name = x.Group.Name

        }).ToList();

        return new PagedResult<GetGroupInvitesDto>(groupInvites, groupInvites.Count, pageSize, request.PageNumber);
    }
}