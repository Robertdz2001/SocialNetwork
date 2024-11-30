using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Domain.Entities;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.GroupRequests.InviteToGroup;

public class InviteToGroupRequestHandler : IRequestHandler<InviteToGroupRequest>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserContextService _userContextService;
    private readonly IGroupRepository _groupRepository;

    public InviteToGroupRequestHandler(IUserRepository userRepository, IUserContextService userContextService, IGroupRepository groupRepository)
    {
        _userRepository = userRepository;
        _userContextService = userContextService;
        _groupRepository = groupRepository;
    }

    public async Task Handle(InviteToGroupRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var userToInvite = await _userRepository.GetUserById(request.UserId)
            ?? throw new NotFoundException("User was not found");

        var group = await _groupRepository.GetGroupById(request.GroupId)
            ?? throw new NotFoundException("Group was not found");

        if (group.OwnerId != loggedUserId)
        {
            throw new UnauthorizedException("User is not owner of the group");
        }

        if (!userToInvite.Groups.Any(x => x.Id == group.Id) && !userToInvite.GroupInvites.Any(x => x.GroupId == group.Id)) 
        {
            userToInvite.GroupInvites.Add(new GroupInvite
            {
                ReceiverId = userToInvite.Id,
                Receiver = userToInvite,
                GroupId = group.Id,
                Group = group
            });
        }

        await _userRepository.Update(userToInvite);
    }
}