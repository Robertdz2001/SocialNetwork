using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.GroupRequests.GetGroupsForInvite;

public class GetGroupsForInviteRequestHandler : IRequestHandler<GetGroupsForInviteRequest, List<GetGroupsForInviteDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserContextService _userContextService;

    public GetGroupsForInviteRequestHandler(IUserRepository userRepository, IUserContextService userContextService)
    {
        _userRepository = userRepository;
        _userContextService = userContextService;
    }

    public async Task<List<GetGroupsForInviteDto>> Handle(GetGroupsForInviteRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var loggedUser = await _userRepository.GetUserById(loggedUserId)
            ?? throw new NotFoundException("User was not found");

        var userToInvite = await _userRepository.GetUserById(request.UserId)
            ?? throw new NotFoundException("User was not found");

        var ownedGroups = loggedUser.Groups.Where(x => x.OwnerId == loggedUserId).ToList();

        var userToInviteGroups = userToInvite.Groups;

        var userToInviteInvitedGroups = userToInvite.GroupInvites;

        var groupsForInvite = ownedGroups
            .Where(ownedGroup => !userToInviteGroups.Any(inviteGroup => inviteGroup.Id == ownedGroup.Id) && !userToInviteInvitedGroups.Any(invitedGroup => invitedGroup.GroupId == ownedGroup.Id))
            .ToList();

        return groupsForInvite.Select(group => new GetGroupsForInviteDto
        {
            Id = group.Id,
            Name = group.Name
        }).ToList();
    }
}