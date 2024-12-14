using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Domain.Enums;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.GroupRequests.DeleteGroup;

public class DeleteGroupRequestHandler : IRequestHandler<DeleteGroupRequest>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserContextService _userContextService;
    private readonly IUserRepository _userRepository;

    public DeleteGroupRequestHandler(IGroupRepository groupRepository, IUserContextService userContextService, IUserRepository userRepository)
    {
        _groupRepository = groupRepository;
        _userContextService = userContextService;
        _userRepository = userRepository;
    }

    public async Task Handle(DeleteGroupRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var loggedUser = await _userRepository.GetUserById(loggedUserId)
            ?? throw new NotFoundException("User was not found");

        var group = await _groupRepository.GetGroupById(request.Id)
            ?? throw new NotFoundException("Group was not found");

        if (group.OwnerId != loggedUserId && loggedUser.RoleId != (long)UserRoles.Admin) 
        {
            throw new UnauthorizedException("User is not owner of the group");
        }

        await _groupRepository.DeleteGroup(group);
    }
}