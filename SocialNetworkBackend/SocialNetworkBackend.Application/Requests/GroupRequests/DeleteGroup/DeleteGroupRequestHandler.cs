using MediatR;
using SocialNetworkBackend.Application.Repositories;
using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Shared.Exceptions;

namespace SocialNetworkBackend.Application.Requests.GroupRequests.DeleteGroup;

public class DeleteGroupRequestHandler : IRequestHandler<DeleteGroupRequest>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserContextService _userContextService;

    public DeleteGroupRequestHandler(IGroupRepository groupRepository, IUserContextService userContextService)
    {
        _groupRepository = groupRepository;
        _userContextService = userContextService;
    }

    public async Task Handle(DeleteGroupRequest request, CancellationToken cancellationToken)
    {
        var loggedUserId = _userContextService.GetUserId()
            ?? throw new BadRequestException("User is not logged in");

        var group = await _groupRepository.GetGroupById(request.Id)
            ?? throw new NotFoundException("Group was not found");

        if (group.OwnerId != loggedUserId) 
        {
            throw new UnauthorizedException("User is not owner of the group");
        }

        await _groupRepository.DeleteGroup(group);
    }
}