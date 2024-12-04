using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Requests.GroupRequests.DeleteGroup;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.GroupRequests.DeleteMemberFromGroup;

public class DeleteMemberFromGroupRequestHandler : IRequestHandler<DeleteMemberFromGroupRequest>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserContextService _userContextService;
    private readonly IUserRepository _userRepository;

    public DeleteMemberFromGroupRequestHandler(IGroupRepository groupRepository, IUserContextService userContextService, IUserRepository userRepository)
    {
        _groupRepository = groupRepository;
        _userContextService = userContextService;
        _userRepository = userRepository;
    }

    public async Task Handle(DeleteMemberFromGroupRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var group = await _groupRepository.GetGroupById(request.GroupId)
            ?? throw new NotFoundException("Group was not found");

        if (group.OwnerId != loggedUserId)
        {
            throw new UnauthorizedException("User is not owner of the group");
        }

        var member = await _userRepository.GetUserById(request.MemberId)
            ?? throw new NotFoundException("User was not found");

        if (member.Id == group.OwnerId)
        {
            throw new BadRequestException("Owner can not be deleted from group");
        }

        group.Members.Remove(member);
        await _groupRepository.UpdateGroup(group);
    }
}